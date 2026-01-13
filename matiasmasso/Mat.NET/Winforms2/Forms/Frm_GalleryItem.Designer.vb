<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_GalleryItem
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.TextBoxFeatures = New System.Windows.Forms.TextBox()
        Me.Xl_ImageMime1 = New Mat.Net.Xl_ImageMime()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_ImageMime1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 470)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(932, 48)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(603, 6)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(156, 37)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(770, 6)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(156, 37)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(9, 6)
        Me.ButtonDel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(156, 37)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 20)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Nom"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(92, 18)
        Me.TextBoxNom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(838, 26)
        Me.TextBoxNom.TabIndex = 43
        '
        'TextBoxFeatures
        '
        Me.TextBoxFeatures.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFeatures.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxFeatures.Location = New System.Drawing.Point(9, 371)
        Me.TextBoxFeatures.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBoxFeatures.Multiline = True
        Me.TextBoxFeatures.Name = "TextBoxFeatures"
        Me.TextBoxFeatures.ReadOnly = True
        Me.TextBoxFeatures.Size = New System.Drawing.Size(915, 91)
        Me.TextBoxFeatures.TabIndex = 45
        '
        'Xl_ImageMime1
        '
        Me.Xl_ImageMime1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ImageMime1.Location = New System.Drawing.Point(0, 77)
        Me.Xl_ImageMime1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Xl_ImageMime1.Name = "Xl_ImageMime1"
        Me.Xl_ImageMime1.Size = New System.Drawing.Size(928, 284)
        Me.Xl_ImageMime1.TabIndex = 46
        Me.Xl_ImageMime1.TabStop = False
        '
        'Frm_GalleryItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 518)
        Me.Controls.Add(Me.Xl_ImageMime1)
        Me.Controls.Add(Me.TextBoxFeatures)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Frm_GalleryItem"
        Me.Text = "Imatge de la Galeria"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_ImageMime1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxFeatures As System.Windows.Forms.TextBox
    Friend WithEvents Xl_ImageMime1 As Xl_ImageMime
End Class
