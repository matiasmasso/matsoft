<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_WortenOrder
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TextBox_Order_Id = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_created_date = New System.Windows.Forms.TextBox()
        Me.TextBox_fullname = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_WortenOrderLines1 = New Mat.Net.Xl_WortenOrderLines()
        Me.PanelButtons.SuspendLayout()
        CType(Me.Xl_WortenOrderLines1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 267)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(604, 31)
        Me.PanelButtons.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(385, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(496, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TextBox_Order_Id
        '
        Me.TextBox_Order_Id.Enabled = False
        Me.TextBox_Order_Id.Location = New System.Drawing.Point(15, 32)
        Me.TextBox_Order_Id.Name = "TextBox_Order_Id"
        Me.TextBox_Order_Id.Size = New System.Drawing.Size(81, 20)
        Me.TextBox_Order_Id.TabIndex = 58
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Comanda"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(99, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Data"
        '
        'TextBox_created_date
        '
        Me.TextBox_created_date.Enabled = False
        Me.TextBox_created_date.Location = New System.Drawing.Point(102, 32)
        Me.TextBox_created_date.Name = "TextBox_created_date"
        Me.TextBox_created_date.Size = New System.Drawing.Size(117, 20)
        Me.TextBox_created_date.TabIndex = 61
        '
        'TextBox_fullname
        '
        Me.TextBox_fullname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_fullname.Enabled = False
        Me.TextBox_fullname.Location = New System.Drawing.Point(225, 32)
        Me.TextBox_fullname.Name = "TextBox_fullname"
        Me.TextBox_fullname.Size = New System.Drawing.Size(375, 20)
        Me.TextBox_fullname.TabIndex = 62
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(222, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Client"
        '
        'Xl_WortenOrderLines1
        '
        Me.Xl_WortenOrderLines1.AllowUserToAddRows = False
        Me.Xl_WortenOrderLines1.AllowUserToDeleteRows = False
        Me.Xl_WortenOrderLines1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_WortenOrderLines1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WortenOrderLines1.DisplayObsolets = False
        Me.Xl_WortenOrderLines1.Filter = Nothing
        Me.Xl_WortenOrderLines1.Location = New System.Drawing.Point(15, 58)
        Me.Xl_WortenOrderLines1.MouseIsDown = False
        Me.Xl_WortenOrderLines1.Name = "Xl_WortenOrderLines1"
        Me.Xl_WortenOrderLines1.ReadOnly = True
        Me.Xl_WortenOrderLines1.Size = New System.Drawing.Size(585, 203)
        Me.Xl_WortenOrderLines1.TabIndex = 64
        '
        'Frm_WortenOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 298)
        Me.Controls.Add(Me.Xl_WortenOrderLines1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox_fullname)
        Me.Controls.Add(Me.TextBox_created_date)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.TextBox_Order_Id)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_WortenOrder"
        Me.Text = "Comanda de Worten"
        Me.PanelButtons.ResumeLayout(False)
        CType(Me.Xl_WortenOrderLines1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents TextBox_Order_Id As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox_created_date As TextBox
    Friend WithEvents TextBox_fullname As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_WortenOrderLines1 As Xl_WortenOrderLines
End Class
