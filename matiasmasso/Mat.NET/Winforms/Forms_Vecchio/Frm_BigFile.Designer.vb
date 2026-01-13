<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BigFile
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
        Dim BigFileNew1 As maxisrvr.BigFileNew = New maxisrvr.BigFileNew()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxFch = New System.Windows.Forms.TextBox()
        Me.TextBoxSrcGuid = New System.Windows.Forms.TextBox()
        Me.TextBoxStreamGuid = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_BigFile1 = New Xl_BigFile()
        Me.ComboBoxSrc = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 548)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(369, 31)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(150, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(261, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'TextBoxFch
        '
        Me.TextBoxFch.Location = New System.Drawing.Point(15, 517)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.ReadOnly = True
        Me.TextBoxFch.Size = New System.Drawing.Size(350, 20)
        Me.TextBoxFch.TabIndex = 47
        '
        'TextBoxSrcGuid
        '
        Me.TextBoxSrcGuid.Location = New System.Drawing.Point(75, 39)
        Me.TextBoxSrcGuid.Name = "TextBoxSrcGuid"
        Me.TextBoxSrcGuid.Size = New System.Drawing.Size(290, 20)
        Me.TextBoxSrcGuid.TabIndex = 48
        '
        'TextBoxStreamGuid
        '
        Me.TextBoxStreamGuid.Location = New System.Drawing.Point(75, 65)
        Me.TextBoxStreamGuid.Name = "TextBoxStreamGuid"
        Me.TextBoxStreamGuid.ReadOnly = True
        Me.TextBoxStreamGuid.Size = New System.Drawing.Size(290, 20)
        Me.TextBoxStreamGuid.TabIndex = 49
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Document"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Stream"
        '
        'Xl_BigFile1
        '
        BigFileNew1.Height = 0
        BigFileNew1.Hres = 0
        BigFileNew1.Img = Nothing
        BigFileNew1.MimeCod = DTOEnums.MimeCods.NotSet
        BigFileNew1.Pags = 0
        BigFileNew1.Size = 0
        BigFileNew1.Stream = Nothing
        BigFileNew1.Vres = 0
        BigFileNew1.Width = 0
        Me.Xl_BigFile1.BigFile = BigFileNew1
        Me.Xl_BigFile1.Location = New System.Drawing.Point(15, 91)
        Me.Xl_BigFile1.Name = "Xl_BigFile1"
        Me.Xl_BigFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_BigFile1.TabIndex = 46
        '
        'ComboBoxSrc
        '
        Me.ComboBoxSrc.FormattingEnabled = True
        Me.ComboBoxSrc.Location = New System.Drawing.Point(75, 12)
        Me.ComboBoxSrc.Name = "ComboBoxSrc"
        Me.ComboBoxSrc.Size = New System.Drawing.Size(290, 21)
        Me.ComboBoxSrc.TabIndex = 52
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(28, 13)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Codi"
        '
        'Frm_BigFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(369, 579)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBoxSrc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxStreamGuid)
        Me.Controls.Add(Me.TextBoxSrcGuid)
        Me.Controls.Add(Me.TextBoxFch)
        Me.Controls.Add(Me.Xl_BigFile1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_BigFile"
        Me.Text = "BIGFILE"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Xl_BigFile1 As Xl_Bigfile
    Friend WithEvents TextBoxFch As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSrcGuid As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxStreamGuid As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxSrc As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
