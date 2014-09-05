using EnvDTE;
using EnvDTE80;
using Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

#if DEBUG
namespace CommonAddInDebug
#else
namespace CommonAddIn
#endif
{
	public class CommandAttribute : Attribute
	{
		public string name;
		public string binding;

		public CommandAttribute(string tbinding)
		{
			name = null;
			binding = tbinding;
		}

		public CommandAttribute(string tname, string tbinding)
		{
			name = tname;
			binding = tbinding;
		}
	}

	public class Connect : IDTExtensibility2, IDTCommandTarget
	{
		private class CommandExec
		{
			public Command command;
			public MethodInfo method;
		}

		private DTE2 application;
		private AddIn addIn;
		private Dictionary<string, CommandExec> commands = new Dictionary<string, CommandExec>();

		public DTE2 Application { get { return application; } }
		public AddIn AddIn { get { return addIn; } }

		public void OnConnection(object tapplication, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			application = (DTE2)tapplication;
			addIn = (AddIn)addInInst;

			Init();
		}

		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		public void OnAddInsUpdate(ref Array custom)
		{
		}

		public void OnStartupComplete(ref Array custom)
		{

		}

		public void OnBeginShutdown(ref Array custom)
		{
		}

		public void Exec(string CmdName, vsCommandExecOption ExecuteOption, ref object VariantIn, ref object VariantOut, ref bool Handled)
		{
			CommandExec result;
			if (commands.TryGetValue(CmdName, out result)) {
				try {
					bool undoOpen = application.UndoContext.IsOpen;
					if (!undoOpen) {
						application.UndoContext.Open(CmdName);
					}
					result.method.Invoke(null, new object[] { this });
					if (!undoOpen) {
						application.UndoContext.Close();
					}
				} catch (Exception e) {
					MessageBox.Show("Error at Exec: " + e.ToString());
				}
				Handled = true;
			} else {
				MessageBox.Show("Command not found: " + CmdName);
				Handled = false;
			}
		}

		public void QueryStatus(string CmdName, vsCommandStatusTextWanted NeededText, ref vsCommandStatus StatusOption, ref object CommandText)
		{
			StatusOption = vsCommandStatus.vsCommandStatusEnabled | vsCommandStatus.vsCommandStatusSupported;
		}

		void Init()
		{
			try {
				StringBuilder builder = new StringBuilder();

				var types = Assembly.GetExecutingAssembly().GetTypes();
				foreach (var type in types) {
					var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
					foreach (var method in methods) {
						CommandAttribute attr = method.GetCustomAttribute<CommandAttribute>();
						if (attr != null) {
							AddCommand(attr.name == null ? method.Name : attr.name, attr.binding, method);
						}
					}
				}

			} catch (Exception e) {
				MessageBox.Show("Error at Init: " + e.ToString());
			}

//			string result = "";
//			foreach (var key in commands.Keys) {
//				result += key + "\n";
//			}
//			MessageBox.Show(result);
		}

		void AddCommand(string name, string binding, MethodInfo method)
		{
			string fullName = addIn.ProgID + "." + name;
			if (commands.ContainsKey(fullName)) {
				MessageBox.Show("A command with the same name already exists: " + name);
				return;
			}

			Command result = null;
			var parameters = method.GetParameters();
			if (!(parameters.Length == 1
				&& parameters[0].ParameterType == typeof(Connect))) {
				return;
			}

			try {
				result = application.Commands.Item(fullName);
			} catch (Exception) { }

			if (result == null) {
				try {
					result = application.Commands.AddNamedCommand(addIn, name, name, name, false);
				} catch (Exception) { MessageBox.Show("2"); }
			}

			if (result != null) {
				try {
					result.Bindings = binding;
				} catch (Exception e) {
					MessageBox.Show("Binding is invalid: " + binding + "\n" + e.ToString());
				}

				commands.Add(fullName, new CommandExec {
					command = result,
					method = method
				});
			}
		}

		bool BindCommand(string name, string binding)
		{
			Command command = null;
			try {
				command = application.Commands.Item(name);
			} catch (Exception) { }

			if (command != null) {
				var bindings = command.Bindings as object[];
				bindings.Concat(new object[] { binding });
				command.Bindings = bindings;

				return true;
			} else {
				return false;
			}
		}
	}
}