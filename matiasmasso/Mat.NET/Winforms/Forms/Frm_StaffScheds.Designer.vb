<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_StaffScheds
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
        Me.Xl_StaffScheds1 = New Winforms.Xl_StaffScheds()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBoxTitular = New System.Windows.Forms.TextBox()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Xl_StaffHolidays1 = New Winforms.Xl_StaffHolidays()
        CType(Me.Xl_StaffScheds1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_StaffHolidays1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_StaffScheds1
        '
        Me.Xl_StaffScheds1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_StaffScheds1.DisplayObsolets = False
        Me.Xl_StaffScheds1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StaffScheds1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_StaffScheds1.MouseIsDown = False
        Me.Xl_StaffScheds1.Name = "Xl_StaffScheds1"
        Me.Xl_StaffScheds1.Size = New System.Drawing.Size(326, 363)
        Me.Xl_StaffScheds1.TabIndex = 3
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(1, 54)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(340, 395)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_StaffScheds1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(332, 369)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Calendaris"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_StaffHolidays1)
        Me.TabPage2.Controls.Add(Me.WebBrowser1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(332, 369)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Festius"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TextBoxTitular
        '
        Me.TextBoxTitular.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTitular.Location = New System.Drawing.Point(5, 22)
        Me.TextBoxTitular.Name = "TextBoxTitular"
        Me.TextBoxTitular.ReadOnly = True
        Me.TextBoxTitular.Size = New System.Drawing.Size(329, 20)
        Me.TextBoxTitular.TabIndex = 5
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(3, 3)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(326, 363)
        Me.WebBrowser1.TabIndex = 0
        '
        'Xl_StaffHolidays1
        '
        Me.Xl_StaffHolidays1.AllowUserToAddRows = False
        Me.Xl_StaffHolidays1.AllowUserToDeleteRows = False
        Me.Xl_StaffHolidays1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_StaffHolidays1.DisplayObsolets = False
        Me.Xl_StaffHolidays1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StaffHolidays1.Filter = Nothing
        Me.Xl_StaffHolidays1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_StaffHolidays1.MouseIsDown = False
        Me.Xl_StaffHolidays1.Name = "Xl_StaffHolidays1"
        Me.Xl_StaffHolidays1.ReadOnly = True
        Me.Xl_StaffHolidays1.Size = New System.Drawing.Size(326, 363)
        Me.Xl_StaffHolidays1.TabIndex = 1
        '
        'Frm_StaffScheds
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 450)
        Me.Controls.Add(Me.TextBoxTitular)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_StaffScheds"
        Me.Text = "Horaris de Jornada"
        CType(Me.Xl_StaffScheds1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_StaffHolidays1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_StaffScheds1 As Xl_StaffScheds
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TextBoxTitular As TextBox
    Friend WithEvents Xl_StaffHolidays1 As Xl_StaffHolidays
    Friend WithEvents WebBrowser1 As WebBrowser
End Class
