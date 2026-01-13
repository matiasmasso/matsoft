<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ImportacioItem
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SplitContainerPdf = New System.Windows.Forms.SplitContainer()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile_Old()
        Me.CheckedListBoxDocSrc = New System.Windows.Forms.CheckedListBox()
        Me.ButtonSavePdf = New System.Windows.Forms.Button()
        Me.TextBoxDescripcio = New System.Windows.Forms.TextBox()
        Me.LabelDescripcio = New System.Windows.Forms.Label()
        CType(Me.SplitContainerPdf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerPdf.Panel1.SuspendLayout()
        Me.SplitContainerPdf.Panel2.SuspendLayout()
        Me.SplitContainerPdf.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerPdf
        '
        Me.SplitContainerPdf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerPdf.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerPdf.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerPdf.Name = "SplitContainerPdf"
        '
        'SplitContainerPdf.Panel1
        '
        Me.SplitContainerPdf.Panel1.Controls.Add(Me.Xl_DocFile1)
        '
        'SplitContainerPdf.Panel2
        '
        Me.SplitContainerPdf.Panel2.Controls.Add(Me.LabelDescripcio)
        Me.SplitContainerPdf.Panel2.Controls.Add(Me.TextBoxDescripcio)
        Me.SplitContainerPdf.Panel2.Controls.Add(Me.CheckedListBoxDocSrc)
        Me.SplitContainerPdf.Panel2.Controls.Add(Me.ButtonSavePdf)
        Me.SplitContainerPdf.Size = New System.Drawing.Size(662, 442)
        Me.SplitContainerPdf.SplitterDistance = 352
        Me.SplitContainerPdf.TabIndex = 2
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 3
        '
        'CheckedListBoxDocSrc
        '
        Me.CheckedListBoxDocSrc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBoxDocSrc.FormattingEnabled = True
        Me.CheckedListBoxDocSrc.Location = New System.Drawing.Point(0, 0)
        Me.CheckedListBoxDocSrc.Name = "CheckedListBoxDocSrc"
        Me.CheckedListBoxDocSrc.Size = New System.Drawing.Size(306, 304)
        Me.CheckedListBoxDocSrc.TabIndex = 2
        '
        'ButtonSavePdf
        '
        Me.ButtonSavePdf.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonSavePdf.Location = New System.Drawing.Point(0, 399)
        Me.ButtonSavePdf.Name = "ButtonSavePdf"
        Me.ButtonSavePdf.Size = New System.Drawing.Size(306, 43)
        Me.ButtonSavePdf.TabIndex = 1
        Me.ButtonSavePdf.Text = "REGISTRAR DOCUMENT"
        Me.ButtonSavePdf.UseVisualStyleBackColor = True
        '
        'TextBoxDescripcio
        '
        Me.TextBoxDescripcio.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDescripcio.Location = New System.Drawing.Point(0, 348)
        Me.TextBoxDescripcio.Name = "TextBoxDescripcio"
        Me.TextBoxDescripcio.Size = New System.Drawing.Size(303, 20)
        Me.TextBoxDescripcio.TabIndex = 3
        Me.TextBoxDescripcio.Visible = False
        '
        'LabelDescripcio
        '
        Me.LabelDescripcio.AutoSize = True
        Me.LabelDescripcio.Location = New System.Drawing.Point(3, 332)
        Me.LabelDescripcio.Name = "LabelDescripcio"
        Me.LabelDescripcio.Size = New System.Drawing.Size(57, 13)
        Me.LabelDescripcio.TabIndex = 4
        Me.LabelDescripcio.Text = "Descripció"
        Me.LabelDescripcio.Visible = False
        '
        'Frm_ImportacioItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 442)
        Me.Controls.Add(Me.SplitContainerPdf)
        Me.Name = "Frm_ImportacioItem"
        Me.Text = "Frm_ImportacioItem"
        Me.SplitContainerPdf.Panel1.ResumeLayout(False)
        Me.SplitContainerPdf.Panel2.ResumeLayout(False)
        Me.SplitContainerPdf.Panel2.PerformLayout()
        CType(Me.SplitContainerPdf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerPdf.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerPdf As System.Windows.Forms.SplitContainer
    Friend WithEvents ButtonSavePdf As System.Windows.Forms.Button
    Friend WithEvents CheckedListBoxDocSrc As System.Windows.Forms.CheckedListBox
    Friend WithEvents Xl_DocFile1 As Winforms.Xl_DocFile_Old
    Friend WithEvents LabelDescripcio As System.Windows.Forms.Label
    Friend WithEvents TextBoxDescripcio As System.Windows.Forms.TextBox
End Class
