using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TX_Text_Control_Format_Painter
{
    public partial class Form1 : Form
    {
        ArrayList myFormatAL = new ArrayList();
        int customStyle = 1;
        string RTF = @"{\rtf1\ansi\ansicpg1252\uc1\deff0{\fonttbl {\f0\fswiss\fcharset0\fprq2 Arial;}{\f1\fswiss\fcharset0\fprq2 Tahoma;}{\f2\fmodern\fcharset0\fprq1 Courier New;}{\f3\froman\fcharset2\fprq2 Symbol;}}{\colortbl;\red0\green0\blue0;\red255\green255\blue255;}{\stylesheet{\s0\itap0\nowidctlpar\f0\fs24 [Normal];}{\*\cs10\additive Default Paragraph Font;}}{\*\generator TX_RTF32 14.0.520.501;}\deftab1134\paperw12240\paperh15840\margl1138\margt1138\margr1138\margb1138\widowctrl\formshade\sectd\headery720\footery720\pgwsxn12240\pghsxn15840\marglsxn1138\margtsxn1138\margrsxn1138\margbsxn1138\pard\itap0\nowidctlpar\plain\f1\fs32\b Simply right click somewhere on the text and navigate through the context menu to the Format Painter feature.\par\plain\f0\fs24\i Please make sure that you actually select text with the same formatting when copying.\par\plain\f2\fs48\ul You can load and save documents with the File menu.\par }";
        public Form1()
        {
            InitializeComponent();
        }
        
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textControl1.Load();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textControl1.Save();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            textControl1.ButtonBar = buttonBar1;
            textControl1.RulerBar = rulerBar1;
            textControl1.VerticalRulerBar = rulerBar2;
            textControl1.StatusBar = statusBar1;
            textControl1.Load(RTF, TXTextControl.StringStreamType.RichTextFormat);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (textControl1.Selection.Length == 0)
            {
                cutToolStripMenuItem1.Enabled = false;
                copyToolStripMenuItem1.Enabled = false;
                deleteToolStripMenuItem1.Enabled = false;
                pasteFormatToolStripMenuItem.Enabled = false;
            }
            else
            {
                cutToolStripMenuItem1.Enabled = true;
                copyToolStripMenuItem1.Enabled = true;
                deleteToolStripMenuItem1.Enabled = true;
                pasteFormatToolStripMenuItem.Enabled = true;
            }
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textControl1.Cut();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textControl1.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textControl1.Paste();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textControl1.Clear();
        }

        private void copyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            copyFormat();
        }

        private void textControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show((Control)sender, e.X, e.Y);
            }
        }

        private void formatPaintToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            copyToolStripMenuItem2.Enabled = textControl1.Selection.IsCommonValueSelected(TXTextControl.Selection.Attribute.All);
            createStyleFromFToolStripMenuItem.Enabled = textControl1.Selection.IsCommonValueSelected(TXTextControl.Selection.Attribute.All);
            if ((textControl1.Selection.Length == 0) | (myFormatAL.Count < 1))
            {
                pasteFormatToolStripMenuItem.Enabled = false;
            }
            else 
            {
                pasteFormatToolStripMenuItem.Enabled = true;
            }
        }

        private void pasteFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteFormat();
        }

        private void createStyleFromFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyFormatToStyle();
        }

        private void copyFormat()
        {
            //check if text format can be copied, e.g. if the formatting is the same for all characters
            if (textControl1.Selection.IsCommonValueSelected(TXTextControl.Selection.Attribute.All))
            {
                //clear the ArrayList before using it
                myFormatAL.Clear();
                //copy each formatting attribute
                myFormatAL.Add(textControl1.Selection.Baseline);
                myFormatAL.Add(textControl1.Selection.Bold);
                myFormatAL.Add(textControl1.Selection.FontName);
                myFormatAL.Add(textControl1.Selection.FontSize);
                myFormatAL.Add(textControl1.Selection.ForeColor);
                myFormatAL.Add(textControl1.Selection.Italic);
                myFormatAL.Add(textControl1.Selection.Strikeout);
                myFormatAL.Add(textControl1.Selection.TextBackColor);
                myFormatAL.Add(textControl1.Selection.Underline);
            }
        }

        private void pasteFormat()
        {
            //check if a format has been copied
            if (myFormatAL.Count >= 1)
            {
                //query each formatting attribute and use the correct type by casting
                textControl1.Selection.Baseline = (int)myFormatAL[0];
                textControl1.Selection.Bold = (bool)myFormatAL[1];
                textControl1.Selection.FontName = (string)myFormatAL[2];
                textControl1.Selection.FontSize = (int)myFormatAL[3];
                textControl1.Selection.ForeColor = (Color)myFormatAL[4];
                textControl1.Selection.Italic = (bool)myFormatAL[5];
                textControl1.Selection.Strikeout = (bool)myFormatAL[6];
                textControl1.Selection.TextBackColor = (Color)myFormatAL[7];
                textControl1.Selection.Underline = (TXTextControl.FontUnderlineStyle)myFormatAL[8];
            }
        }
        private void copyFormatToStyle()
        {
            //clear the ArrayList before using it
            myFormatAL.Clear();
            myFormatAL.Add(textControl1.Selection.Baseline);
            myFormatAL.Add(textControl1.Selection.Bold);
            myFormatAL.Add(textControl1.Selection.FontName);
            myFormatAL.Add(textControl1.Selection.FontSize);
            myFormatAL.Add(textControl1.Selection.ForeColor);
            myFormatAL.Add(textControl1.Selection.Italic);
            myFormatAL.Add(textControl1.Selection.Strikeout);
            myFormatAL.Add(textControl1.Selection.TextBackColor);
            myFormatAL.Add(textControl1.Selection.Underline);

            //create a new custom style based on the stored formatting attributes
            TXTextControl.InlineStyle iStyle = new TXTextControl.InlineStyle("custom" + customStyle.ToString());
            iStyle.Baseline = textControl1.Selection.Baseline;
            iStyle.Bold = textControl1.Selection.Bold;
            iStyle.FontName = textControl1.Selection.FontName;
            iStyle.FontSize = textControl1.Selection.FontSize;
            iStyle.ForeColor = textControl1.Selection.ForeColor;
            iStyle.Italic = textControl1.Selection.Italic;
            iStyle.Strikeout = textControl1.Selection.Strikeout;
            iStyle.TextBackColor = textControl1.Selection.TextBackColor;
            iStyle.Underline = textControl1.Selection.Underline;

            //add the new custom style to the TextControl
            textControl1.InlineStyles.Add(iStyle);
            customStyle += 1;
            //use the new custom style for the current selection
            textControl1.Selection.FormattingStyle = iStyle.Name;
        }

        
    }
}