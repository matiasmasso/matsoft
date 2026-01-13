<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ProductDownload
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBoxSrcs = New System.Windows.Forms.ComboBox()
        Me.CheckBoxPublicarAlConsumidor = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPublicarAlDistribuidor = New System.Windows.Forms.CheckBox()
        Me.CheckBoxLang = New System.Windows.Forms.CheckBox()
        Me.CheckBoxObsolet = New System.Windows.Forms.CheckBox()
        Me.Xl_Langs1 = New Mat.Net.Xl_Langs()
        Me.Xl_BaseGuidCodNoms1 = New Mat.Net.Xl_BaseGuidCodNoms()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Mat.Net.Xl_DocFile()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_LangSet1 = New Mat.Net.Xl_LangSet()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_BaseGuidCodNoms1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 421)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(720, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(501, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(612, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
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
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 47)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Categoría"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(64, 20)
        Me.TextBoxNom.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(302, 20)
        Me.TextBoxNom.TabIndex = 48
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 22)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Nom"
        '
        'ComboBoxSrcs
        '
        Me.ComboBoxSrcs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxSrcs.FormattingEnabled = True
        Me.ComboBoxSrcs.Location = New System.Drawing.Point(64, 47)
        Me.ComboBoxSrcs.Margin = New System.Windows.Forms.Padding(1)
        Me.ComboBoxSrcs.Name = "ComboBoxSrcs"
        Me.ComboBoxSrcs.Size = New System.Drawing.Size(303, 21)
        Me.ComboBoxSrcs.TabIndex = 49
        '
        'CheckBoxPublicarAlConsumidor
        '
        Me.CheckBoxPublicarAlConsumidor.AutoSize = True
        Me.CheckBoxPublicarAlConsumidor.Location = New System.Drawing.Point(64, 77)
        Me.CheckBoxPublicarAlConsumidor.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxPublicarAlConsumidor.Name = "CheckBoxPublicarAlConsumidor"
        Me.CheckBoxPublicarAlConsumidor.Size = New System.Drawing.Size(132, 17)
        Me.CheckBoxPublicarAlConsumidor.TabIndex = 50
        Me.CheckBoxPublicarAlConsumidor.Text = "Publicar al consumidor"
        Me.CheckBoxPublicarAlConsumidor.UseVisualStyleBackColor = True
        '
        'CheckBoxPublicarAlDistribuidor
        '
        Me.CheckBoxPublicarAlDistribuidor.AutoSize = True
        Me.CheckBoxPublicarAlDistribuidor.Location = New System.Drawing.Point(64, 106)
        Me.CheckBoxPublicarAlDistribuidor.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxPublicarAlDistribuidor.Name = "CheckBoxPublicarAlDistribuidor"
        Me.CheckBoxPublicarAlDistribuidor.Size = New System.Drawing.Size(128, 17)
        Me.CheckBoxPublicarAlDistribuidor.TabIndex = 51
        Me.CheckBoxPublicarAlDistribuidor.Text = "Publicar al distribuidor"
        Me.CheckBoxPublicarAlDistribuidor.UseVisualStyleBackColor = True
        '
        'CheckBoxLang
        '
        Me.CheckBoxLang.AutoSize = True
        Me.CheckBoxLang.Location = New System.Drawing.Point(64, 135)
        Me.CheckBoxLang.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxLang.Name = "CheckBoxLang"
        Me.CheckBoxLang.Size = New System.Drawing.Size(105, 17)
        Me.CheckBoxLang.TabIndex = 52
        Me.CheckBoxLang.Text = "Idioma especific:"
        Me.CheckBoxLang.UseVisualStyleBackColor = True
        '
        'CheckBoxObsolet
        '
        Me.CheckBoxObsolet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxObsolet.AutoSize = True
        Me.CheckBoxObsolet.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxObsolet.Location = New System.Drawing.Point(304, 403)
        Me.CheckBoxObsolet.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxObsolet.Name = "CheckBoxObsolet"
        Me.CheckBoxObsolet.Size = New System.Drawing.Size(62, 17)
        Me.CheckBoxObsolet.TabIndex = 54
        Me.CheckBoxObsolet.Text = "Obsolet"
        Me.CheckBoxObsolet.UseVisualStyleBackColor = True
        '
        'Xl_Langs1
        '
        Me.Xl_Langs1.Enabled = False
        Me.Xl_Langs1.Location = New System.Drawing.Point(171, 133)
        Me.Xl_Langs1.Name = "Xl_Langs1"
        Me.Xl_Langs1.Size = New System.Drawing.Size(50, 21)
        Me.Xl_Langs1.TabIndex = 53
        Me.Xl_Langs1.Value = Nothing
        '
        'Xl_BaseGuidCodNoms1
        '
        Me.Xl_BaseGuidCodNoms1.AllowUserToAddRows = False
        Me.Xl_BaseGuidCodNoms1.AllowUserToDeleteRows = False
        Me.Xl_BaseGuidCodNoms1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_BaseGuidCodNoms1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_BaseGuidCodNoms1.DisplayObsolets = False
        Me.Xl_BaseGuidCodNoms1.Filter = Nothing
        Me.Xl_BaseGuidCodNoms1.Location = New System.Drawing.Point(6, 249)
        Me.Xl_BaseGuidCodNoms1.MouseIsDown = False
        Me.Xl_BaseGuidCodNoms1.Name = "Xl_BaseGuidCodNoms1"
        Me.Xl_BaseGuidCodNoms1.ReadOnly = True
        Me.Xl_BaseGuidCodNoms1.Size = New System.Drawing.Size(361, 150)
        Me.Xl_BaseGuidCodNoms1.TabIndex = 55
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 223)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Targets:"
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1._DefaultMimeCod = MatHelperStd.MimeCods.Pdf
        Me.Xl_DocFile1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.IsInedit = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(370, 0)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 57
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 175)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(206, 13)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Publicar a la web en els següents idiomes:"
        '
        'Xl_LangSet1
        '
        Me.Xl_LangSet1.Location = New System.Drawing.Point(64, 192)
        Me.Xl_LangSet1.Name = "Xl_LangSet1"
        Me.Xl_LangSet1.Size = New System.Drawing.Size(176, 24)
        Me.Xl_LangSet1.TabIndex = 59
        '
        'Frm_ProductDownload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(720, 452)
        Me.Controls.Add(Me.Xl_LangSet1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_BaseGuidCodNoms1)
        Me.Controls.Add(Me.CheckBoxObsolet)
        Me.Controls.Add(Me.Xl_Langs1)
        Me.Controls.Add(Me.CheckBoxLang)
        Me.Controls.Add(Me.CheckBoxPublicarAlDistribuidor)
        Me.Controls.Add(Me.CheckBoxPublicarAlConsumidor)
        Me.Controls.Add(Me.ComboBoxSrcs)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.Name = "Frm_ProductDownload"
        Me.Text = "Descàrrega"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_BaseGuidCodNoms1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBoxSrcs As ComboBox
    Friend WithEvents CheckBoxPublicarAlConsumidor As CheckBox
    Friend WithEvents CheckBoxPublicarAlDistribuidor As CheckBox
    Friend WithEvents CheckBoxLang As CheckBox
    Friend WithEvents Xl_Langs1 As Xl_Langs
    Friend WithEvents CheckBoxObsolet As CheckBox
    Friend WithEvents Xl_BaseGuidCodNoms1 As Xl_BaseGuidCodNoms
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_DocFile1 As Xl_DocFile
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_LangSet1 As Xl_LangSet
End Class
