using EnvDTE;
using EnvDTE80;
using Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

//#if DEBUG
//namespace CommonVsAddInDebug
//#else
namespace CommonVsAddIn
//#endif
{
	static class CommonTools
	{
		[Command("Text Editor::Alt+Right Arrow")]
		static void MoveRightByCamelCase(Connect connect)
		{
			MoveRightByCamelCaseBase(connect, false);
		}

		[Command("Text Editor::Alt+Shift+Right Arrow")]
		static void MoveRightByCamelCaseExtend(Connect connect)
		{
			MoveRightByCamelCaseBase(connect, true);
		}

		[Command("Text Editor::Alt+Left Arrow")]
		static void MoveLeftByCamelCase(Connect connect)
		{
			MoveLeftByCamelCaseBase(connect, false);
		}

		[Command("Text Editor::Alt+Shift+Left Arrow")]
		static void MoveLeftByCamelCaseExtend(Connect connect)
		{
			MoveLeftByCamelCaseBase(connect, true);
		}

		[Command("Text Editor::Alt+BkSpce")]
		static void DeleteLeftByCamelCase(Connect connect)
		{
			MoveLeftByCamelCaseBase(connect, true);
			var selection = connect.Application.ActiveDocument.Selection as TextSelection;
			selection.Delete();
		}

		[Command("Text Editor::Alt+Del")]
		static void DeleteRightByCamelCase(Connect connect)
		{
			MoveRightByCamelCaseBase(connect, true);
			var selection = connect.Application.ActiveDocument.Selection as TextSelection;
			selection.Delete();
		}

		static void MoveRightByCamelCaseBase(Connect connect, bool extend)
		{
			var selection = connect.Application.ActiveDocument.Selection as TextSelection;
			var point = selection.ActivePoint.CreateEditPoint();
			point.CharRight(1);

			char left, right;
			while (true) {
				left = GetChar(point);
				point.CharRight(1);
				right = GetChar(point);

				if (IsCamelCase(left, right) || point.AtEndOfDocument) {
					break;
				}
			}

			selection.MoveToPoint(point, extend);
		}

		static void MoveLeftByCamelCaseBase(Connect connect, bool extend)
		{
			var selection = connect.Application.ActiveDocument.Selection as TextSelection;
			var point = selection.ActivePoint.CreateEditPoint();
			point.CharLeft(1);

			char left, right;
			while (true) {
				right = GetChar(point);
				point.CharLeft(1);
				left = GetChar(point);

				if (IsCamelCase(left, right)) {
					point.CharRight(1);
					break;
				} else if (point.AtEndOfDocument) {
					break;
				}
			}

			selection.MoveToPoint(point, extend);
		}

		static char GetChar(EditPoint point)
		{
			var str = point.GetText(1);
			if (String.IsNullOrEmpty(str)) {
				return ' ';
			} else {
				return str[0];
			}
		}

		static bool IsCamelCase(char left, char right)
		{
			return Char.IsLetter(left) && (!Char.IsLetter(right) || (Char.IsLower(left) && Char.IsUpper(right)))
					|| Char.IsDigit(left) && !Char.IsDigit(right)
					|| Char.IsWhiteSpace(left) && !Char.IsWhiteSpace(right)
					|| Char.IsPunctuation(left) && !Char.IsPunctuation(right);
		}

		static Regex toggleCommentRegex = new Regex(@"^(\s*)(//)(.*)", RegexOptions.Compiled);
		[Command("Text Editor::Ctrl+Alt+C")]
		static void ToggleComment(Connect connect)
		{
			var selection = connect.Application.ActiveDocument.Selection as TextSelection;

			int firstLine = selection.TopPoint.Line;
			int lastLine = selection.BottomPoint.Line;
			var lineCount = 1 + lastLine - firstLine;

			selection.GotoLine(firstLine, true);
			selection.LineDown(true, lastLine - firstLine);
			selection.EndOfLine(true);

			var matches = new List<Match>(lineCount);
			bool allLinesCommented = true;
			for (var iline = firstLine; iline <= lastLine; iline++) {
				selection.GotoLine(iline, true);
				var match = toggleCommentRegex.Match(selection.Text);
				matches.Add(match);
				allLinesCommented &= match.Success;
			}

			if (allLinesCommented) {
				for (var iline = firstLine; iline <= lastLine; iline++) {
					var match = matches[iline - firstLine];
					if (match.Success) {
						selection.GotoLine(iline, true);
						selection.Text = match.Groups[1].Value + match.Groups[3].Value;
					}
				}
			} else {
				for (var iline = firstLine; iline <= lastLine; iline++) {
					var match = matches[iline - firstLine];
					if (!match.Success) {
						selection.GotoLine(iline, false);
						selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn);
						selection.Text = "//";
					}
				}
			}

			selection.GotoLine(firstLine, true);
			selection.LineDown(true, lastLine - firstLine);
			selection.EndOfLine(true);
		}

		[Command("Text Editor::Alt+PgDn")]
		static void HalfPageDown(Connect connect)
		{
			var selection = connect.Application.ActiveDocument.Selection as TextSelection;
			int line = selection.ActivePoint.Line;
			selection.PageDown(false);
			int nextLine = selection.ActivePoint.Line;
			selection.GotoLine((nextLine + line) >> 1);
		}

		[Command("Text Editor::Alt+PgUp")]
		static void HalfPageUp(Connect connect)
		{
			var selection = connect.Application.ActiveDocument.Selection as TextSelection;
			int line = selection.ActivePoint.Line;
			selection.PageUp(false);
			int nextLine = selection.ActivePoint.Line;
			selection.GotoLine((nextLine + line) >> 1);
		}

		static Regex joinLinesRegex = new Regex(@"(\n|\r\n?|\r|\s{2,})", RegexOptions.Compiled | RegexOptions.Multiline);
		[Command("Text Editor::Ctrl+Shift+J")]
		static void JoinLines(Connect connect)
		{
			var selection = connect.Application.ActiveDocument.Selection as TextSelection;
			var topLine = selection.TopPoint.Line;
			var bottomLine = selection.BottomPoint.Line;
			selection.GotoLine(topLine, true);
			selection.LineDown(true, bottomLine - topLine);
			selection.EndOfLine(true);

			var text = selection.Text;
			var firstSpace = joinLinesRegex.Match(text);
			selection.Text = firstSpace + joinLinesRegex.Replace(text.Substring(firstSpace.Length), "");
		}

		[Command("Global::Alt+Shift+F1")]
		static void Test(Connect connect)
		{

		}
	}
}
