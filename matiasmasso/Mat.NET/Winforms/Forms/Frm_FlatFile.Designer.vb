<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_FlatFile
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBoxFieldLengths = New System.Windows.Forms.TextBox()
        Me.Xl_FlatFileSegments1 = New Winforms.Xl_FlatFileSegments()
        Me.ImportarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_FlatFileSegments1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(657, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportarToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'TextBoxFieldLengths
        '
        Me.TextBoxFieldLengths.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFieldLengths.Location = New System.Drawing.Point(0, 48)
        Me.TextBoxFieldLengths.Name = "TextBoxFieldLengths"
        Me.TextBoxFieldLengths.Size = New System.Drawing.Size(657, 20)
        Me.TextBoxFieldLengths.TabIndex = 1
        '
        'Xl_FlatFileSegments1
        '
        Me.Xl_FlatFileSegments1.AllowUserToAddRows = False
        Me.Xl_FlatFileSegments1.AllowUserToDeleteRows = False
        Me.Xl_FlatFileSegments1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_FlatFileSegments1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_FlatFileSegments1.DisplayObsolets = False
        Me.Xl_FlatFileSegments1.Location = New System.Drawing.Point(0, 74)
        Me.Xl_FlatFileSegments1.MouseIsDown = False
        Me.Xl_FlatFileSegments1.Name = "Xl_FlatFileSegments1"
        Me.Xl_FlatFileSegments1.ReadOnly = True
        Me.Xl_FlatFileSegments1.Size = New System.Drawing.Size(657, 262)
        Me.Xl_FlatFileSegments1.TabIndex = 2
        '
        'ImportarToolStripMenuItem
        '
        Me.ImportarToolStripMenuItem.Name = "ImportarToolStripMenuItem"
        Me.ImportarToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ImportarToolStripMenuItem.Text = "Importar"
        '
        'Frm_FlatFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(657, 336)
        Me.Controls.Add(Me.Xl_FlatFileSegments1)
        Me.Controls.Add(Me.TextBoxFieldLengths)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_FlatFile"
        Me.Text = "Flat file"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_FlatFileSegments1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBoxFieldLengths As TextBox
    Friend WithEvents Xl_FlatFileSegments1 As Xl_FlatFileSegments
    Friend WithEvents ImportarToolStripMenuItem As ToolStripMenuItem
End Class
