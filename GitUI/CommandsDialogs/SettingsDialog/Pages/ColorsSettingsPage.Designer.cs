﻿namespace GitUI.CommandsDialogs.SettingsDialog.Pages
{
    partial class ColorsSettingsPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TableLayoutPanel tlpnlMain;
            this.gbRevisionGraph = new System.Windows.Forms.GroupBox();
            this.tlpnlRevisionGraph = new System.Windows.Forms.TableLayoutPanel();
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel = new System.Windows.Forms.Label();
            this.lblColorHighlightAuthored = new System.Windows.Forms.Label();
            this.chkHighlightAuthored = new System.Windows.Forms.CheckBox();
            this.MulticolorBranches = new System.Windows.Forms.CheckBox();
            this._NO_TRANSLATE_ColorRemoteBranchLabel = new System.Windows.Forms.Label();
            this.lblColorBranchRemote = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_ColorOtherLabel = new System.Windows.Forms.Label();
            this.lblColorLabel = new System.Windows.Forms.Label();
            this.chkDrawAlternateBackColor = new System.Windows.Forms.CheckBox();
            this.DrawNonRelativesTextGray = new System.Windows.Forms.CheckBox();
            this._NO_TRANSLATE_ColorGraphLabel = new System.Windows.Forms.Label();
            this.DrawNonRelativesGray = new System.Windows.Forms.CheckBox();
            this.lblColorBranchLocal = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_ColorBranchLabel = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_ColorTagLabel = new System.Windows.Forms.Label();
            this.lblColorTag = new System.Windows.Forms.Label();
            this.gbDiffView = new System.Windows.Forms.GroupBox();
            this.tlpnlDiffView = new System.Windows.Forms.TableLayoutPanel();
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel = new System.Windows.Forms.Label();
            this.lblColorLineRemoved = new System.Windows.Forms.Label();
            this.lblColorSection = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_ColorSectionLabel = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_ColorAddedLineDiffLabel = new System.Windows.Forms.Label();
            this.lblColorHilghlightLineAdded = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel = new System.Windows.Forms.Label();
            this.lblColorHilghlightLineRemoved = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_ColorRemovedLine = new System.Windows.Forms.Label();
            this.lblColorLineAdded = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_ColorAddedLineLabel = new System.Windows.Forms.Label();
            this.lblColorHighlightAllOccurrences = new System.Windows.Forms.Label();
            this.gbTheme = new System.Windows.Forms.GroupBox();
            this.fpnlTheme = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRestartNeeded = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_cbSelectTheme = new System.Windows.Forms.ComboBox();
            this.btnTheme = new System.Windows.Forms.Button();
            this.btnResetTheme = new System.Windows.Forms.Button();
            this.chkUseSystemVisualStyle = new System.Windows.Forms.CheckBox();
            tlpnlMain = new System.Windows.Forms.TableLayoutPanel();
            tlpnlMain.SuspendLayout();
            this.gbRevisionGraph.SuspendLayout();
            this.tlpnlRevisionGraph.SuspendLayout();
            this.gbDiffView.SuspendLayout();
            this.tlpnlDiffView.SuspendLayout();
            this.gbTheme.SuspendLayout();
            this.fpnlTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpnlMain
            // 
            tlpnlMain.AutoSize = true;
            tlpnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tlpnlMain.ColumnCount = 1;
            tlpnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tlpnlMain.Controls.Add(this.gbRevisionGraph, 0, 0);
            tlpnlMain.Controls.Add(this.gbDiffView, 0, 1);
            tlpnlMain.Controls.Add(this.gbTheme, 0, 2);
            tlpnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpnlMain.Location = new System.Drawing.Point(8, 8);
            tlpnlMain.Name = "tlpnlMain";
            tlpnlMain.RowCount = 4;
            tlpnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tlpnlMain.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpnlMain.Size = new System.Drawing.Size(1012, 647);
            tlpnlMain.TabIndex = 0;
            // 
            // gbRevisionGraph
            // 
            this.gbRevisionGraph.AutoSize = true;
            this.gbRevisionGraph.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbRevisionGraph.Controls.Add(this.tlpnlRevisionGraph);
            this.gbRevisionGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRevisionGraph.Location = new System.Drawing.Point(3, 3);
            this.gbRevisionGraph.Name = "gbRevisionGraph";
            this.gbRevisionGraph.Padding = new System.Windows.Forms.Padding(8);
            this.gbRevisionGraph.Size = new System.Drawing.Size(1006, 262);
            this.gbRevisionGraph.TabIndex = 0;
            this.gbRevisionGraph.TabStop = false;
            this.gbRevisionGraph.Text = "Revision graph";
            // 
            // tlpnlRevisionGraph
            // 
            this.tlpnlRevisionGraph.AutoSize = true;
            this.tlpnlRevisionGraph.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpnlRevisionGraph.ColumnCount = 3;
            this.tlpnlRevisionGraph.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlRevisionGraph.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlRevisionGraph.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlRevisionGraph.Controls.Add(this._NO_TRANSLATE_ColorHighlightAuthoredLabel, 1,
                6);
            this.tlpnlRevisionGraph.Controls.Add(this.lblColorHighlightAuthored, 0, 6);
            this.tlpnlRevisionGraph.Controls.Add(this.chkHighlightAuthored, 0, 5);
            this.tlpnlRevisionGraph.Controls.Add(this.MulticolorBranches, 0, 0);
            this.tlpnlRevisionGraph.Controls.Add(this._NO_TRANSLATE_ColorRemoteBranchLabel, 1, 9);
            this.tlpnlRevisionGraph.Controls.Add(this.lblColorBranchRemote, 0, 9);
            this.tlpnlRevisionGraph.Controls.Add(this._NO_TRANSLATE_ColorOtherLabel, 1, 10);
            this.tlpnlRevisionGraph.Controls.Add(this.lblColorLabel, 0, 10);
            this.tlpnlRevisionGraph.Controls.Add(this.chkDrawAlternateBackColor, 0, 1);
            this.tlpnlRevisionGraph.Controls.Add(this.DrawNonRelativesTextGray, 0, 4);
            this.tlpnlRevisionGraph.Controls.Add(this._NO_TRANSLATE_ColorGraphLabel, 1, 0);
            this.tlpnlRevisionGraph.Controls.Add(this.DrawNonRelativesGray, 0, 3);
            this.tlpnlRevisionGraph.Controls.Add(this.lblColorBranchLocal, 0, 8);
            this.tlpnlRevisionGraph.Controls.Add(this._NO_TRANSLATE_ColorBranchLabel, 1, 8);
            this.tlpnlRevisionGraph.Controls.Add(this._NO_TRANSLATE_ColorTagLabel, 1, 7);
            this.tlpnlRevisionGraph.Controls.Add(this.lblColorTag, 0, 7);
            this.tlpnlRevisionGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpnlRevisionGraph.Location = new System.Drawing.Point(8, 24);
            this.tlpnlRevisionGraph.Name = "tlpnlRevisionGraph";
            this.tlpnlRevisionGraph.RowCount = 12;
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlRevisionGraph.Size = new System.Drawing.Size(990, 230);
            this.tlpnlRevisionGraph.TabIndex = 0;
            // 
            // _NO_TRANSLATE_ColorHighlightAuthoredLabel
            // 
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.Cursor =
                System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.Dock =
                System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.Enabled = false;
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.Location =
                new System.Drawing.Point(192, 125);
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.Name =
                "_NO_TRANSLATE_ColorHighlightAuthoredLabel";
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.TabIndex = 7;
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.Text = "Red";
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorHighlightAuthoredLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // lblColorHighlightAuthored
            // 
            this.lblColorHighlightAuthored.AutoSize = true;
            this.lblColorHighlightAuthored.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorHighlightAuthored.Enabled = false;
            this.lblColorHighlightAuthored.Location = new System.Drawing.Point(3, 128);
            this.lblColorHighlightAuthored.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorHighlightAuthored.Name = "lblColorHighlightAuthored";
            this.lblColorHighlightAuthored.Size = new System.Drawing.Size(183, 15);
            this.lblColorHighlightAuthored.TabIndex = 6;
            this.lblColorHighlightAuthored.Text = "Color authored revisions";
            // 
            // chkHighlightAuthored
            // 
            this.chkHighlightAuthored.AutoSize = true;
            this.chkHighlightAuthored.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkHighlightAuthored.Location = new System.Drawing.Point(3, 103);
            this.chkHighlightAuthored.Name = "chkHighlightAuthored";
            this.chkHighlightAuthored.Size = new System.Drawing.Size(183, 19);
            this.chkHighlightAuthored.TabIndex = 5;
            this.chkHighlightAuthored.Text = "Highlight authored revisions";
            this.chkHighlightAuthored.UseVisualStyleBackColor = true;
            this.chkHighlightAuthored.CheckedChanged +=
                new System.EventHandler(this.ChkHighlightAuthored_CheckedChanged);
            // 
            // MulticolorBranches
            // 
            this.MulticolorBranches.AutoSize = true;
            this.MulticolorBranches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MulticolorBranches.Location = new System.Drawing.Point(3, 3);
            this.MulticolorBranches.Name = "MulticolorBranches";
            this.MulticolorBranches.Size = new System.Drawing.Size(183, 19);
            this.MulticolorBranches.TabIndex = 0;
            this.MulticolorBranches.Text = "Multicolor branches";
            this.MulticolorBranches.UseVisualStyleBackColor = true;
            this.MulticolorBranches.CheckedChanged +=
                new System.EventHandler(this.MulticolorBranches_CheckedChanged);
            // 
            // _NO_TRANSLATE_ColorRemoteBranchLabel
            // 
            this._NO_TRANSLATE_ColorRemoteBranchLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorRemoteBranchLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorRemoteBranchLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorRemoteBranchLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorRemoteBranchLabel.Location = new System.Drawing.Point(192, 188);
            this._NO_TRANSLATE_ColorRemoteBranchLabel.Name = "_NO_TRANSLATE_ColorRemoteBranchLabel";
            this._NO_TRANSLATE_ColorRemoteBranchLabel.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorRemoteBranchLabel.TabIndex = 13;
            this._NO_TRANSLATE_ColorRemoteBranchLabel.Text = "Red";
            this._NO_TRANSLATE_ColorRemoteBranchLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorRemoteBranchLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // lblColorBranchRemote
            // 
            this.lblColorBranchRemote.AutoSize = true;
            this.lblColorBranchRemote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBranchRemote.Location = new System.Drawing.Point(3, 191);
            this.lblColorBranchRemote.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorBranchRemote.Name = "lblColorBranchRemote";
            this.lblColorBranchRemote.Size = new System.Drawing.Size(183, 15);
            this.lblColorBranchRemote.TabIndex = 12;
            this.lblColorBranchRemote.Text = "Color remote branch";
            // 
            // _NO_TRANSLATE_ColorOtherLabel
            // 
            this._NO_TRANSLATE_ColorOtherLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorOtherLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorOtherLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorOtherLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorOtherLabel.Location = new System.Drawing.Point(192, 209);
            this._NO_TRANSLATE_ColorOtherLabel.Name = "_NO_TRANSLATE_ColorOtherLabel";
            this._NO_TRANSLATE_ColorOtherLabel.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorOtherLabel.TabIndex = 15;
            this._NO_TRANSLATE_ColorOtherLabel.Text = "Red";
            this._NO_TRANSLATE_ColorOtherLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorOtherLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // lblColorLabel
            // 
            this.lblColorLabel.AutoSize = true;
            this.lblColorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorLabel.Location = new System.Drawing.Point(3, 212);
            this.lblColorLabel.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorLabel.Name = "lblColorLabel";
            this.lblColorLabel.Size = new System.Drawing.Size(183, 15);
            this.lblColorLabel.TabIndex = 14;
            this.lblColorLabel.Text = "Color other label";
            // 
            // chkDrawAlternateBackColor
            // 
            this.chkDrawAlternateBackColor.AutoSize = true;
            this.chkDrawAlternateBackColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDrawAlternateBackColor.Location = new System.Drawing.Point(3, 28);
            this.chkDrawAlternateBackColor.Name = "chkDrawAlternateBackColor";
            this.chkDrawAlternateBackColor.Size = new System.Drawing.Size(183, 19);
            this.chkDrawAlternateBackColor.TabIndex = 2;
            this.chkDrawAlternateBackColor.Text = "Draw alternate background";
            this.chkDrawAlternateBackColor.UseVisualStyleBackColor = true;
            // 
            // DrawNonRelativesTextGray
            // 
            this.DrawNonRelativesTextGray.AutoSize = true;
            this.DrawNonRelativesTextGray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawNonRelativesTextGray.Location = new System.Drawing.Point(3, 78);
            this.DrawNonRelativesTextGray.Name = "DrawNonRelativesTextGray";
            this.DrawNonRelativesTextGray.Size = new System.Drawing.Size(183, 19);
            this.DrawNonRelativesTextGray.TabIndex = 4;
            this.DrawNonRelativesTextGray.Text = "Draw non relatives text gray";
            this.DrawNonRelativesTextGray.UseVisualStyleBackColor = true;
            // 
            // _NO_TRANSLATE_ColorGraphLabel
            // 
            this._NO_TRANSLATE_ColorGraphLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorGraphLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorGraphLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorGraphLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorGraphLabel.Location = new System.Drawing.Point(192, 0);
            this._NO_TRANSLATE_ColorGraphLabel.Name = "_NO_TRANSLATE_ColorGraphLabel";
            this._NO_TRANSLATE_ColorGraphLabel.Size = new System.Drawing.Size(27, 25);
            this._NO_TRANSLATE_ColorGraphLabel.TabIndex = 1;
            this._NO_TRANSLATE_ColorGraphLabel.Text = "Red";
            this._NO_TRANSLATE_ColorGraphLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorGraphLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // DrawNonRelativesGray
            // 
            this.DrawNonRelativesGray.AutoSize = true;
            this.DrawNonRelativesGray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawNonRelativesGray.Location = new System.Drawing.Point(3, 53);
            this.DrawNonRelativesGray.Name = "DrawNonRelativesGray";
            this.DrawNonRelativesGray.Size = new System.Drawing.Size(183, 19);
            this.DrawNonRelativesGray.TabIndex = 3;
            this.DrawNonRelativesGray.Text = "Draw non relatives graph gray";
            this.DrawNonRelativesGray.UseVisualStyleBackColor = true;
            // 
            // lblColorBranchLocal
            // 
            this.lblColorBranchLocal.AutoSize = true;
            this.lblColorBranchLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBranchLocal.Location = new System.Drawing.Point(3, 170);
            this.lblColorBranchLocal.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorBranchLocal.Name = "lblColorBranchLocal";
            this.lblColorBranchLocal.Size = new System.Drawing.Size(183, 15);
            this.lblColorBranchLocal.TabIndex = 10;
            this.lblColorBranchLocal.Text = "Color branch";
            // 
            // _NO_TRANSLATE_ColorBranchLabel
            // 
            this._NO_TRANSLATE_ColorBranchLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorBranchLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorBranchLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorBranchLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorBranchLabel.Location = new System.Drawing.Point(192, 167);
            this._NO_TRANSLATE_ColorBranchLabel.Name = "_NO_TRANSLATE_ColorBranchLabel";
            this._NO_TRANSLATE_ColorBranchLabel.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorBranchLabel.TabIndex = 11;
            this._NO_TRANSLATE_ColorBranchLabel.Text = "Red";
            this._NO_TRANSLATE_ColorBranchLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorBranchLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // _NO_TRANSLATE_ColorTagLabel
            // 
            this._NO_TRANSLATE_ColorTagLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorTagLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorTagLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorTagLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorTagLabel.Location = new System.Drawing.Point(192, 146);
            this._NO_TRANSLATE_ColorTagLabel.Name = "_NO_TRANSLATE_ColorTagLabel";
            this._NO_TRANSLATE_ColorTagLabel.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorTagLabel.TabIndex = 9;
            this._NO_TRANSLATE_ColorTagLabel.Text = "Red";
            this._NO_TRANSLATE_ColorTagLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorTagLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // lblColorTag
            // 
            this.lblColorTag.AutoSize = true;
            this.lblColorTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorTag.Location = new System.Drawing.Point(3, 149);
            this.lblColorTag.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorTag.Name = "lblColorTag";
            this.lblColorTag.Size = new System.Drawing.Size(183, 15);
            this.lblColorTag.TabIndex = 8;
            this.lblColorTag.Text = "Color tag";
            // 
            // gbDiffView
            // 
            this.gbDiffView.AutoSize = true;
            this.gbDiffView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbDiffView.Controls.Add(this.tlpnlDiffView);
            this.gbDiffView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDiffView.Location = new System.Drawing.Point(3, 271);
            this.gbDiffView.Name = "gbDiffView";
            this.gbDiffView.Padding = new System.Windows.Forms.Padding(8);
            this.gbDiffView.Size = new System.Drawing.Size(1006, 158);
            this.gbDiffView.TabIndex = 1;
            this.gbDiffView.TabStop = false;
            this.gbDiffView.Text = "Difference view";
            // 
            // tlpnlDiffView
            // 
            this.tlpnlDiffView.AutoSize = true;
            this.tlpnlDiffView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpnlDiffView.ColumnCount = 3;
            this.tlpnlDiffView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlDiffView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlDiffView.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlDiffView.Controls.Add(this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel, 1,
                5);
            this.tlpnlDiffView.Controls.Add(this.lblColorLineRemoved, 0, 0);
            this.tlpnlDiffView.Controls.Add(this.lblColorSection, 0, 4);
            this.tlpnlDiffView.Controls.Add(this._NO_TRANSLATE_ColorSectionLabel, 1, 4);
            this.tlpnlDiffView.Controls.Add(this._NO_TRANSLATE_ColorAddedLineDiffLabel, 1, 3);
            this.tlpnlDiffView.Controls.Add(this.lblColorHilghlightLineAdded, 0, 3);
            this.tlpnlDiffView.Controls.Add(this._NO_TRANSLATE_ColorRemovedLineDiffLabel, 1, 2);
            this.tlpnlDiffView.Controls.Add(this.lblColorHilghlightLineRemoved, 0, 2);
            this.tlpnlDiffView.Controls.Add(this._NO_TRANSLATE_ColorRemovedLine, 1, 0);
            this.tlpnlDiffView.Controls.Add(this.lblColorLineAdded, 0, 1);
            this.tlpnlDiffView.Controls.Add(this._NO_TRANSLATE_ColorAddedLineLabel, 1, 1);
            this.tlpnlDiffView.Controls.Add(this.lblColorHighlightAllOccurrences, 0, 5);
            this.tlpnlDiffView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpnlDiffView.Location = new System.Drawing.Point(8, 24);
            this.tlpnlDiffView.Name = "tlpnlDiffView";
            this.tlpnlDiffView.RowCount = 6;
            this.tlpnlDiffView.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlDiffView.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlDiffView.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlDiffView.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlDiffView.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlDiffView.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpnlDiffView.Size = new System.Drawing.Size(990, 126);
            this.tlpnlDiffView.TabIndex = 0;
            // 
            // _NO_TRANSLATE_ColorHighlightAllOccurrencesLabel
            // 
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.BackColor =
                System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.Cursor =
                System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.Dock =
                System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.Location =
                new System.Drawing.Point(185, 105);
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.Name =
                "_NO_TRANSLATE_ColorHighlightAllOccurrencesLabel";
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.Size =
                new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.TabIndex = 11;
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.Text = "Red";
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorHighlightAllOccurrencesLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // lblColorLineRemoved
            // 
            this.lblColorLineRemoved.AutoSize = true;
            this.lblColorLineRemoved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorLineRemoved.Location = new System.Drawing.Point(3, 3);
            this.lblColorLineRemoved.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorLineRemoved.Name = "lblColorLineRemoved";
            this.lblColorLineRemoved.Size = new System.Drawing.Size(176, 15);
            this.lblColorLineRemoved.TabIndex = 0;
            this.lblColorLineRemoved.Text = "Color removed line";
            // 
            // lblColorSection
            // 
            this.lblColorSection.AutoSize = true;
            this.lblColorSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorSection.Location = new System.Drawing.Point(3, 87);
            this.lblColorSection.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorSection.Name = "lblColorSection";
            this.lblColorSection.Size = new System.Drawing.Size(176, 15);
            this.lblColorSection.TabIndex = 8;
            this.lblColorSection.Text = "Color section";
            // 
            // _NO_TRANSLATE_ColorSectionLabel
            // 
            this._NO_TRANSLATE_ColorSectionLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorSectionLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorSectionLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorSectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorSectionLabel.Location = new System.Drawing.Point(185, 84);
            this._NO_TRANSLATE_ColorSectionLabel.Name = "_NO_TRANSLATE_ColorSectionLabel";
            this._NO_TRANSLATE_ColorSectionLabel.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorSectionLabel.TabIndex = 9;
            this._NO_TRANSLATE_ColorSectionLabel.Text = "Red";
            this._NO_TRANSLATE_ColorSectionLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorSectionLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // _NO_TRANSLATE_ColorAddedLineDiffLabel
            // 
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.Location = new System.Drawing.Point(185, 63);
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.Name =
                "_NO_TRANSLATE_ColorAddedLineDiffLabel";
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.TabIndex = 7;
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.Text = "Red";
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorAddedLineDiffLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // lblColorHilghlightLineAdded
            // 
            this.lblColorHilghlightLineAdded.AutoSize = true;
            this.lblColorHilghlightLineAdded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorHilghlightLineAdded.Location = new System.Drawing.Point(3, 66);
            this.lblColorHilghlightLineAdded.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorHilghlightLineAdded.Name = "lblColorHilghlightLineAdded";
            this.lblColorHilghlightLineAdded.Size = new System.Drawing.Size(176, 15);
            this.lblColorHilghlightLineAdded.TabIndex = 6;
            this.lblColorHilghlightLineAdded.Text = "Color added line highlighting";
            // 
            // _NO_TRANSLATE_ColorRemovedLineDiffLabel
            // 
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.Location =
                new System.Drawing.Point(185, 42);
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.Name =
                "_NO_TRANSLATE_ColorRemovedLineDiffLabel";
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.TabIndex = 5;
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.Text = "Red";
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorRemovedLineDiffLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // lblColorHilghlightLineRemoved
            // 
            this.lblColorHilghlightLineRemoved.AutoSize = true;
            this.lblColorHilghlightLineRemoved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorHilghlightLineRemoved.Location = new System.Drawing.Point(3, 45);
            this.lblColorHilghlightLineRemoved.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorHilghlightLineRemoved.Name = "lblColorHilghlightLineRemoved";
            this.lblColorHilghlightLineRemoved.Size = new System.Drawing.Size(176, 15);
            this.lblColorHilghlightLineRemoved.TabIndex = 4;
            this.lblColorHilghlightLineRemoved.Text = "Color removed line highlighting";
            // 
            // _NO_TRANSLATE_ColorRemovedLine
            // 
            this._NO_TRANSLATE_ColorRemovedLine.AutoSize = true;
            this._NO_TRANSLATE_ColorRemovedLine.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorRemovedLine.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorRemovedLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorRemovedLine.Location = new System.Drawing.Point(185, 0);
            this._NO_TRANSLATE_ColorRemovedLine.Name = "_NO_TRANSLATE_ColorRemovedLine";
            this._NO_TRANSLATE_ColorRemovedLine.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorRemovedLine.TabIndex = 1;
            this._NO_TRANSLATE_ColorRemovedLine.Text = "Red";
            this._NO_TRANSLATE_ColorRemovedLine.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorRemovedLine.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // lblColorLineAdded
            // 
            this.lblColorLineAdded.AutoSize = true;
            this.lblColorLineAdded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorLineAdded.Location = new System.Drawing.Point(3, 24);
            this.lblColorLineAdded.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorLineAdded.Name = "lblColorLineAdded";
            this.lblColorLineAdded.Size = new System.Drawing.Size(176, 15);
            this.lblColorLineAdded.TabIndex = 2;
            this.lblColorLineAdded.Text = "Color added line";
            // 
            // _NO_TRANSLATE_ColorAddedLineLabel
            // 
            this._NO_TRANSLATE_ColorAddedLineLabel.AutoSize = true;
            this._NO_TRANSLATE_ColorAddedLineLabel.BackColor = System.Drawing.Color.Red;
            this._NO_TRANSLATE_ColorAddedLineLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this._NO_TRANSLATE_ColorAddedLineLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._NO_TRANSLATE_ColorAddedLineLabel.Location = new System.Drawing.Point(185, 21);
            this._NO_TRANSLATE_ColorAddedLineLabel.Name = "_NO_TRANSLATE_ColorAddedLineLabel";
            this._NO_TRANSLATE_ColorAddedLineLabel.Size = new System.Drawing.Size(27, 21);
            this._NO_TRANSLATE_ColorAddedLineLabel.TabIndex = 3;
            this._NO_TRANSLATE_ColorAddedLineLabel.Text = "Red";
            this._NO_TRANSLATE_ColorAddedLineLabel.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this._NO_TRANSLATE_ColorAddedLineLabel.Click +=
                new System.EventHandler(this.ColorLabel_Click);
            // 
            // lblColorHighlightAllOccurrences
            // 
            this.lblColorHighlightAllOccurrences.AutoSize = true;
            this.lblColorHighlightAllOccurrences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorHighlightAllOccurrences.Location = new System.Drawing.Point(3, 108);
            this.lblColorHighlightAllOccurrences.Margin = new System.Windows.Forms.Padding(3);
            this.lblColorHighlightAllOccurrences.Name = "lblColorHighlightAllOccurrences";
            this.lblColorHighlightAllOccurrences.Size = new System.Drawing.Size(176, 15);
            this.lblColorHighlightAllOccurrences.TabIndex = 10;
            this.lblColorHighlightAllOccurrences.Text = "Color highlight all occurences";
            // 
            // gbTheme
            // 
            this.gbTheme.AutoSize = true;
            this.gbTheme.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbTheme.Controls.Add(this.fpnlTheme);
            this.gbTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTheme.Location = new System.Drawing.Point(3, 435);
            this.gbTheme.Name = "gbTheme";
            this.gbTheme.Size = new System.Drawing.Size(1006, 80);
            this.gbTheme.TabIndex = 3;
            this.gbTheme.TabStop = false;
            this.gbTheme.Text = "Color scheme";
            // 
            // fpnlTheme
            // 
            this.fpnlTheme.AutoSize = true;
            this.fpnlTheme.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fpnlTheme.Controls.Add(this.lblRestartNeeded);
            this.fpnlTheme.Controls.Add(this._NO_TRANSLATE_cbSelectTheme);
            this.fpnlTheme.Controls.Add(this.btnTheme);
            this.fpnlTheme.Controls.Add(this.btnResetTheme);
            this.fpnlTheme.Controls.Add(this.chkUseSystemVisualStyle);
            this.fpnlTheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpnlTheme.Location = new System.Drawing.Point(3, 19);
            this.fpnlTheme.Name = "fpnlTheme";
            this.fpnlTheme.Size = new System.Drawing.Size(1000, 58);
            this.fpnlTheme.TabIndex = 0;
            // 
            // lblRestartNeeded
            // 
            this.lblRestartNeeded.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRestartNeeded.AutoSize = true;
            this.fpnlTheme.SetFlowBreak(this.lblRestartNeeded, true);
            this.lblRestartNeeded.Font = new System.Drawing.Font("Segoe UI", 12F,
                System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblRestartNeeded.Image = global::GitUI.Properties.Images.Warning;
            this.lblRestartNeeded.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblRestartNeeded.Location = new System.Drawing.Point(3, 4);
            this.lblRestartNeeded.Name = "lblRestartNeeded";
            this.lblRestartNeeded.Size = new System.Drawing.Size(284, 21);
            this.lblRestartNeeded.TabIndex = 5;
            this.lblRestartNeeded.Text = "     To apply changes restart is necessary";
            // 
            // _NO_TRANSLATE_cbSelectTheme
            // 
            this._NO_TRANSLATE_cbSelectTheme.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._NO_TRANSLATE_cbSelectTheme.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._NO_TRANSLATE_cbSelectTheme.FormattingEnabled = true;
            this._NO_TRANSLATE_cbSelectTheme.Location = new System.Drawing.Point(3, 32);
            this._NO_TRANSLATE_cbSelectTheme.Name = "_NO_TRANSLATE_cbSelectTheme";
            this._NO_TRANSLATE_cbSelectTheme.Size = new System.Drawing.Size(179, 23);
            this._NO_TRANSLATE_cbSelectTheme.TabIndex = 0;
            this._NO_TRANSLATE_cbSelectTheme.SelectedIndexChanged +=
                new System.EventHandler(this.ComboBoxTheme_SelectedIndexChanged);
            // 
            // btnTheme
            // 
            this.btnTheme.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnTheme.Location = new System.Drawing.Point(188, 32);
            this.btnTheme.Name = "btnTheme";
            this.btnTheme.Size = new System.Drawing.Size(64, 23);
            this.btnTheme.TabIndex = 2;
            this.btnTheme.Text = "Edit...";
            this.btnTheme.UseVisualStyleBackColor = true;
            this.btnTheme.Click += new System.EventHandler(this.BtnTheme_Click);
            // 
            // btnResetTheme
            // 
            this.btnResetTheme.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnResetTheme.Location = new System.Drawing.Point(258, 32);
            this.btnResetTheme.Name = "btnResetTheme";
            this.btnResetTheme.Size = new System.Drawing.Size(64, 23);
            this.btnResetTheme.TabIndex = 3;
            this.btnResetTheme.Text = "Reset";
            this.btnResetTheme.UseVisualStyleBackColor = true;
            this.btnResetTheme.Click += new System.EventHandler(this.BtnResetTheme_Click);
            // 
            // chkUseSystemVisualStyle
            // 
            this.chkUseSystemVisualStyle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkUseSystemVisualStyle.AutoSize = true;
            this.chkUseSystemVisualStyle.Location = new System.Drawing.Point(328, 34);
            this.chkUseSystemVisualStyle.Name = "chkUseSystemVisualStyle";
            this.chkUseSystemVisualStyle.Size = new System.Drawing.Size(339, 19);
            this.chkUseSystemVisualStyle.TabIndex = 4;
            this.chkUseSystemVisualStyle.Text =
                "Use system-defined visual style (looks bad with dark colors)";
            this.chkUseSystemVisualStyle.UseVisualStyleBackColor = true;
            // 
            // ColorsSettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(tlpnlMain);
            this.Name = "ColorsSettingsPage";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Size = new System.Drawing.Size(1028, 663);
            tlpnlMain.ResumeLayout(false);
            tlpnlMain.PerformLayout();
            this.gbRevisionGraph.ResumeLayout(false);
            this.gbRevisionGraph.PerformLayout();
            this.tlpnlRevisionGraph.ResumeLayout(false);
            this.tlpnlRevisionGraph.PerformLayout();
            this.gbDiffView.ResumeLayout(false);
            this.gbDiffView.PerformLayout();
            this.tlpnlDiffView.ResumeLayout(false);
            this.tlpnlDiffView.PerformLayout();
            this.gbTheme.ResumeLayout(false);
            this.gbTheme.PerformLayout();
            this.fpnlTheme.ResumeLayout(false);
            this.fpnlTheme.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.GroupBox gbRevisionGraph;
        private System.Windows.Forms.CheckBox DrawNonRelativesTextGray;
        private System.Windows.Forms.CheckBox DrawNonRelativesGray;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorGraphLabel;
        private System.Windows.Forms.CheckBox MulticolorBranches;
        private System.Windows.Forms.Label lblColorBranchRemote;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorRemoteBranchLabel;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorOtherLabel;
        private System.Windows.Forms.Label lblColorLabel;
        private System.Windows.Forms.Label lblColorTag;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorTagLabel;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorBranchLabel;
        private System.Windows.Forms.Label lblColorBranchLocal;
        private System.Windows.Forms.GroupBox gbDiffView;
        private System.Windows.Forms.Label lblColorHilghlightLineRemoved;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorRemovedLineDiffLabel;
        private System.Windows.Forms.Label lblColorHilghlightLineAdded;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorAddedLineDiffLabel;
        private System.Windows.Forms.Label lblColorLineRemoved;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorSectionLabel;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorRemovedLine;
        private System.Windows.Forms.Label lblColorSection;
        private System.Windows.Forms.Label lblColorLineAdded;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorAddedLineLabel;
        private System.Windows.Forms.CheckBox chkDrawAlternateBackColor;
        private System.Windows.Forms.TableLayoutPanel tlpnlRevisionGraph;
        private System.Windows.Forms.TableLayoutPanel tlpnlDiffView;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorHighlightAllOccurrencesLabel;
        private System.Windows.Forms.Label lblColorHighlightAllOccurrences;
        private System.Windows.Forms.Label lblColorHighlightAuthored;
        private System.Windows.Forms.CheckBox chkHighlightAuthored;
        private System.Windows.Forms.Label _NO_TRANSLATE_ColorHighlightAuthoredLabel;
        private System.Windows.Forms.Button btnTheme;
        private System.Windows.Forms.GroupBox gbTheme;
        private System.Windows.Forms.ComboBox _NO_TRANSLATE_cbSelectTheme;
        private System.Windows.Forms.FlowLayoutPanel fpnlTheme;
        private System.Windows.Forms.Button btnResetTheme;
        private System.Windows.Forms.CheckBox chkUseSystemVisualStyle;
        private System.Windows.Forms.Label lblRestartNeeded;
    }
}
