<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_InvoiceReceived
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
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxProveidor = New System.Windows.Forms.TextBox()
        Me.TextBoxDocNum = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ValidateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RetrocedirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_Exceptions1 = New Mat.Net.Xl_Exceptions()
        Me.Xl_InvoiceReceivedItems1 = New Mat.Net.Xl_InvoiceReceivedItems()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.PanelButtons.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_Exceptions1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_InvoiceReceivedItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 419)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(493, 31)
        Me.PanelButtons.TabIndex = 42
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(383, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 15
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(273, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Proveidor"
        '
        'TextBoxProveidor
        '
        Me.TextBoxProveidor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxProveidor.Location = New System.Drawing.Point(15, 47)
        Me.TextBoxProveidor.Name = "TextBoxProveidor"
        Me.TextBoxProveidor.ReadOnly = True
        Me.TextBoxProveidor.Size = New System.Drawing.Size(269, 20)
        Me.TextBoxProveidor.TabIndex = 44
        '
        'TextBoxDocNum
        '
        Me.TextBoxDocNum.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDocNum.Location = New System.Drawing.Point(290, 47)
        Me.TextBoxDocNum.Name = "TextBoxDocNum"
        Me.TextBoxDocNum.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxDocNum.TabIndex = 46
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(287, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Factura"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(393, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Data"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(493, 24)
        Me.MenuStrip1.TabIndex = 49
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ValidateToolStripMenuItem, Me.RetrocedirToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'ValidateToolStripMenuItem
        '
        Me.ValidateToolStripMenuItem.Name = "ValidateToolStripMenuItem"
        Me.ValidateToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.ValidateToolStripMenuItem.Text = "Valida de nou"
        '
        'RetrocedirToolStripMenuItem
        '
        Me.RetrocedirToolStripMenuItem.Name = "RetrocedirToolStripMenuItem"
        Me.RetrocedirToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.RetrocedirToolStripMenuItem.Text = "Retrocedir"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(15, 85)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Exceptions1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_InvoiceReceivedItems1)
        Me.SplitContainer1.Size = New System.Drawing.Size(472, 332)
        Me.SplitContainer1.SplitterDistance = 56
        Me.SplitContainer1.TabIndex = 51
        '
        'Xl_Exceptions1
        '
        Me.Xl_Exceptions1.AllowUserToAddRows = False
        Me.Xl_Exceptions1.AllowUserToDeleteRows = False
        Me.Xl_Exceptions1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Exceptions1.DisplayObsolets = False
        Me.Xl_Exceptions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Exceptions1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Exceptions1.MouseIsDown = False
        Me.Xl_Exceptions1.Name = "Xl_Exceptions1"
        Me.Xl_Exceptions1.ReadOnly = True
        Me.Xl_Exceptions1.Size = New System.Drawing.Size(472, 56)
        Me.Xl_Exceptions1.TabIndex = 0
        '
        'Xl_InvoiceReceivedItems1
        '
        Me.Xl_InvoiceReceivedItems1.AllowUserToAddRows = False
        Me.Xl_InvoiceReceivedItems1.AllowUserToDeleteRows = False
        Me.Xl_InvoiceReceivedItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InvoiceReceivedItems1.DisplayObsolets = False
        Me.Xl_InvoiceReceivedItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_InvoiceReceivedItems1.Filter = Nothing
        Me.Xl_InvoiceReceivedItems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_InvoiceReceivedItems1.MouseIsDown = False
        Me.Xl_InvoiceReceivedItems1.Name = "Xl_InvoiceReceivedItems1"
        Me.Xl_InvoiceReceivedItems1.ReadOnly = True
        Me.Xl_InvoiceReceivedItems1.Size = New System.Drawing.Size(472, 272)
        Me.Xl_InvoiceReceivedItems1.TabIndex = 50
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(396, 47)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(91, 20)
        Me.DateTimePicker1.TabIndex = 52
        '
        'Frm_InvoiceReceived
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(493, 450)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxDocNum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxProveidor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_InvoiceReceived"
        Me.Text = "Factura rebuda"
        Me.PanelButtons.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_Exceptions1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_InvoiceReceivedItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxProveidor As TextBox
    Friend WithEvents TextBoxDocNum As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_InvoiceReceivedItems1 As Xl_InvoiceReceivedItems
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_Exceptions1 As Xl_Exceptions
    Friend WithEvents RetrocedirToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ValidateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ButtonOk As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
End Class
