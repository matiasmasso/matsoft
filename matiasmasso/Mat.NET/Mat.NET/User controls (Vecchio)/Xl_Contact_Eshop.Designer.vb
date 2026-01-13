<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Contact_Eshop
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.GroupBoxActivated = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBoxLink = New System.Windows.Forms.TextBox
        Me.CheckBoxIntegrat = New System.Windows.Forms.CheckBox
        Me.TextBoxTpa = New System.Windows.Forms.TextBox
        Me.CheckedListBoxPunts = New System.Windows.Forms.CheckedListBox
        Me.CheckedListBoxTpa = New System.Windows.Forms.CheckedListBox
        Me.CheckBoxActivated = New System.Windows.Forms.CheckBox
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxWeb = New System.Windows.Forms.TextBox
        Me.Xl_ImageLogo = New Xl_Image
        Me.LabelGuid = New System.Windows.Forms.Label
        Me.GroupBoxActivated.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxActivated
        '
        Me.GroupBoxActivated.Controls.Add(Me.Label3)
        Me.GroupBoxActivated.Controls.Add(Me.TextBoxLink)
        Me.GroupBoxActivated.Controls.Add(Me.CheckBoxIntegrat)
        Me.GroupBoxActivated.Controls.Add(Me.TextBoxTpa)
        Me.GroupBoxActivated.Controls.Add(Me.CheckedListBoxPunts)
        Me.GroupBoxActivated.Controls.Add(Me.CheckedListBoxTpa)
        Me.GroupBoxActivated.Enabled = False
        Me.GroupBoxActivated.Location = New System.Drawing.Point(9, 94)
        Me.GroupBoxActivated.Name = "GroupBoxActivated"
        Me.GroupBoxActivated.Size = New System.Drawing.Size(417, 252)
        Me.GroupBoxActivated.TabIndex = 1
        Me.GroupBoxActivated.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(139, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "enllaç"
        '
        'TextBoxLink
        '
        Me.TextBoxLink.Location = New System.Drawing.Point(178, 81)
        Me.TextBoxLink.Name = "TextBoxLink"
        Me.TextBoxLink.Size = New System.Drawing.Size(233, 20)
        Me.TextBoxLink.TabIndex = 4
        '
        'CheckBoxIntegrat
        '
        Me.CheckBoxIntegrat.AutoSize = True
        Me.CheckBoxIntegrat.Location = New System.Drawing.Point(6, 19)
        Me.CheckBoxIntegrat.Name = "CheckBoxIntegrat"
        Me.CheckBoxIntegrat.Size = New System.Drawing.Size(121, 17)
        Me.CheckBoxIntegrat.TabIndex = 3
        Me.CheckBoxIntegrat.Text = "integració de stocks"
        '
        'TextBoxTpa
        '
        Me.TextBoxTpa.Location = New System.Drawing.Point(139, 55)
        Me.TextBoxTpa.Name = "TextBoxTpa"
        Me.TextBoxTpa.Size = New System.Drawing.Size(272, 20)
        Me.TextBoxTpa.TabIndex = 2
        '
        'CheckedListBoxPunts
        '
        Me.CheckedListBoxPunts.FormattingEnabled = True
        Me.CheckedListBoxPunts.Items.AddRange(New Object() {"enllaç directe a la marca", "marca destacada a pag.inici", "min.4 marques a pagina inici", "enllaç reciproc", "imatge marca ilustrant categoria", "marca preferent dins categoria", "redaccional o banner"})
        Me.CheckedListBoxPunts.Location = New System.Drawing.Point(139, 115)
        Me.CheckedListBoxPunts.Name = "CheckedListBoxPunts"
        Me.CheckedListBoxPunts.Size = New System.Drawing.Size(272, 124)
        Me.CheckedListBoxPunts.TabIndex = 1
        '
        'CheckedListBoxTpa
        '
        Me.CheckedListBoxTpa.FormattingEnabled = True
        Me.CheckedListBoxTpa.Location = New System.Drawing.Point(6, 55)
        Me.CheckedListBoxTpa.Name = "CheckedListBoxTpa"
        Me.CheckedListBoxTpa.Size = New System.Drawing.Size(127, 184)
        Me.CheckedListBoxTpa.TabIndex = 0
        '
        'CheckBoxActivated
        '
        Me.CheckBoxActivated.AutoSize = True
        Me.CheckBoxActivated.Location = New System.Drawing.Point(9, 82)
        Me.CheckBoxActivated.Name = "CheckBoxActivated"
        Me.CheckBoxActivated.Size = New System.Drawing.Size(58, 17)
        Me.CheckBoxActivated.TabIndex = 2
        Me.CheckBoxActivated.Text = "activat"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(48, 21)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(222, 20)
        Me.TextBoxNom.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "nom"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "web"
        '
        'TextBoxWeb
        '
        Me.TextBoxWeb.Location = New System.Drawing.Point(48, 47)
        Me.TextBoxWeb.Name = "TextBoxWeb"
        Me.TextBoxWeb.Size = New System.Drawing.Size(222, 20)
        Me.TextBoxWeb.TabIndex = 5
        '
        'Xl_ImageLogo
        '
        Me.Xl_ImageLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ImageLogo.Bitmap = Nothing
        Me.Xl_ImageLogo.EmptyImageLabelText = ""
        Me.Xl_ImageLogo.Location = New System.Drawing.Point(276, 21)
        Me.Xl_ImageLogo.MaxHeight = 0
        Me.Xl_ImageLogo.MaxWidth = 0
        Me.Xl_ImageLogo.Name = "Xl_ImageLogo"
        Me.Xl_ImageLogo.Size = New System.Drawing.Size(150, 48)
        Me.Xl_ImageLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.Xl_ImageLogo.TabIndex = 23
        Me.Xl_ImageLogo.ZipStream = Nothing
        '
        'LabelGuid
        '
        Me.LabelGuid.AutoSize = True
        Me.LabelGuid.Location = New System.Drawing.Point(12, 359)
        Me.LabelGuid.Name = "LabelGuid"
        Me.LabelGuid.Size = New System.Drawing.Size(29, 13)
        Me.LabelGuid.TabIndex = 24
        Me.LabelGuid.Text = "Guid"
        '
        'Xl_Contact_Eshop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelGuid)
        Me.Controls.Add(Me.Xl_ImageLogo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckBoxActivated)
        Me.Controls.Add(Me.TextBoxWeb)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.GroupBoxActivated)
        Me.Name = "Xl_Contact_Eshop"
        Me.Size = New System.Drawing.Size(435, 410)
        Me.GroupBoxActivated.ResumeLayout(False)
        Me.GroupBoxActivated.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBoxActivated As System.Windows.Forms.GroupBox
    Friend WithEvents CheckedListBoxTpa As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckBoxActivated As System.Windows.Forms.CheckBox
    Friend WithEvents CheckedListBoxPunts As System.Windows.Forms.CheckedListBox
    Friend WithEvents TextBoxTpa As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxWeb As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxIntegrat As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLink As System.Windows.Forms.TextBox
    Friend WithEvents Xl_ImageLogo As Xl_Image
    Friend WithEvents LabelGuid As System.Windows.Forms.Label

End Class
