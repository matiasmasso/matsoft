<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PurchaseOrderConceptShortcut
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
        Me.TextBoxSearchkey = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxPor = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxEng = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxCat = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxEsp = New System.Windows.Forms.TextBox()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_LookupPdcSrc1 = New Mat.Net.Xl_LookupPdcSrc()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PanelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxSearchkey
        '
        Me.TextBoxSearchkey.Location = New System.Drawing.Point(75, 34)
        Me.TextBoxSearchkey.MaxLength = 5
        Me.TextBoxSearchkey.Name = "TextBoxSearchkey"
        Me.TextBoxSearchkey.Size = New System.Drawing.Size(102, 20)
        Me.TextBoxSearchkey.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 72
        Me.Label5.Text = "Shortcut"
        '
        'TextBoxPor
        '
        Me.TextBoxPor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPor.Location = New System.Drawing.Point(75, 189)
        Me.TextBoxPor.Name = "TextBoxPor"
        Me.TextBoxPor.Size = New System.Drawing.Size(383, 20)
        Me.TextBoxPor.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 70
        Me.Label4.Text = "Portuguès"
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEng.Location = New System.Drawing.Point(75, 163)
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(383, 20)
        Me.TextBoxEng.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 166)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = "Anglès"
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCat.Location = New System.Drawing.Point(75, 137)
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(383, 20)
        Me.TextBoxCat.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 140)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 66
        Me.Label2.Text = "Català"
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEsp.Location = New System.Drawing.Point(75, 110)
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(383, 20)
        Me.TextBoxEsp.TabIndex = 2
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 249)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(470, 31)
        Me.PanelButtons.TabIndex = 63
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(251, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 7
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(362, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 6
        Me.ButtonOk.Text = "Acceptar"
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
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 64
        Me.Label1.Text = "Espanyol"
        '
        'Xl_LookupPdcSrc1
        '
        Me.Xl_LookupPdcSrc1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupPdcSrc1.IsDirty = False
        Me.Xl_LookupPdcSrc1.Location = New System.Drawing.Point(75, 61)
        Me.Xl_LookupPdcSrc1.Name = "Xl_LookupPdcSrc1"
        Me.Xl_LookupPdcSrc1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPdcSrc1.ReadOnlyLookup = False
        Me.Xl_LookupPdcSrc1.Size = New System.Drawing.Size(383, 20)
        Me.Xl_LookupPdcSrc1.TabIndex = 1
        Me.Xl_LookupPdcSrc1.Value = DTO.DTOPurchaseOrder.Sources.no_Especificado
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 75
        Me.Label6.Text = "Font"
        '
        'Frm_PurchaseOrderConceptShortcut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(470, 280)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_LookupPdcSrc1)
        Me.Controls.Add(Me.TextBoxSearchkey)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxPor)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxEng)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxCat)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxEsp)
        Me.Controls.Add(Me.PanelButtons)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_PurchaseOrderConceptShortcut"
        Me.Text = "Conceptes frequents de comanda"
        Me.PanelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxSearchkey As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxPor As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxEng As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxCat As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxEsp As TextBox
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_LookupPdcSrc1 As Xl_LookupPdcSrc
    Friend WithEvents Label6 As Label
End Class
