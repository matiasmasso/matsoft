<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EdiManager
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageOrders = New System.Windows.Forms.TabPage()
        Me.Xl_EdiOrders1 = New Mat.Net.Xl_EdiOrders()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RadioButtonInbox = New System.Windows.Forms.RadioButton()
        Me.RadioButtonOutbox = New System.Windows.Forms.RadioButton()
        Me.TabControl1.SuspendLayout()
        Me.TabPageOrders.SuspendLayout()
        CType(Me.Xl_EdiOrders1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageOrders)
        Me.TabControl1.Location = New System.Drawing.Point(0, 48)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(732, 376)
        Me.TabControl1.TabIndex = 0
        '
        'TabPageOrders
        '
        Me.TabPageOrders.Controls.Add(Me.Xl_EdiOrders1)
        Me.TabPageOrders.Location = New System.Drawing.Point(4, 22)
        Me.TabPageOrders.Name = "TabPageOrders"
        Me.TabPageOrders.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageOrders.Size = New System.Drawing.Size(724, 350)
        Me.TabPageOrders.TabIndex = 1
        Me.TabPageOrders.Text = "Order"
        Me.TabPageOrders.UseVisualStyleBackColor = True
        '
        'Xl_EdiOrders1
        '
        Me.Xl_EdiOrders1.AllowUserToAddRows = False
        Me.Xl_EdiOrders1.AllowUserToDeleteRows = False
        Me.Xl_EdiOrders1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiOrders1.DisplayObsolets = False
        Me.Xl_EdiOrders1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiOrders1.Filter = Nothing
        Me.Xl_EdiOrders1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_EdiOrders1.MouseIsDown = False
        Me.Xl_EdiOrders1.Name = "Xl_EdiOrders1"
        Me.Xl_EdiOrders1.ReadOnly = True
        Me.Xl_EdiOrders1.Size = New System.Drawing.Size(718, 344)
        Me.Xl_EdiOrders1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(732, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'RadioButtonInbox
        '
        Me.RadioButtonInbox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonInbox.AutoSize = True
        Me.RadioButtonInbox.Checked = True
        Me.RadioButtonInbox.Location = New System.Drawing.Point(602, 3)
        Me.RadioButtonInbox.Name = "RadioButtonInbox"
        Me.RadioButtonInbox.Size = New System.Drawing.Size(62, 17)
        Me.RadioButtonInbox.TabIndex = 2
        Me.RadioButtonInbox.TabStop = True
        Me.RadioButtonInbox.Text = "Entrada"
        Me.RadioButtonInbox.UseVisualStyleBackColor = True
        '
        'RadioButtonOutbox
        '
        Me.RadioButtonOutbox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButtonOutbox.AutoSize = True
        Me.RadioButtonOutbox.Location = New System.Drawing.Point(670, 3)
        Me.RadioButtonOutbox.Name = "RadioButtonOutbox"
        Me.RadioButtonOutbox.Size = New System.Drawing.Size(58, 17)
        Me.RadioButtonOutbox.TabIndex = 3
        Me.RadioButtonOutbox.TabStop = True
        Me.RadioButtonOutbox.Text = "Sortida"
        Me.RadioButtonOutbox.UseVisualStyleBackColor = True
        '
        'Frm_EdiManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(732, 424)
        Me.Controls.Add(Me.RadioButtonOutbox)
        Me.Controls.Add(Me.RadioButtonInbox)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_EdiManager"
        Me.Text = "Edi Manager"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageOrders.ResumeLayout(False)
        CType(Me.Xl_EdiOrders1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RadioButtonInbox As RadioButton
    Friend WithEvents RadioButtonOutbox As RadioButton
    Friend WithEvents TabPageOrders As TabPage
    Friend WithEvents Xl_EdiOrders1 As Xl_EdiOrders
End Class
