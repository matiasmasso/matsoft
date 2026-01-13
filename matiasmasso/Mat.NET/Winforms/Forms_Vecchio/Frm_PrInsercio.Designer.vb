<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PrInsercio
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxPagina = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ButtonOrdreDeCompra = New System.Windows.Forms.Button()
        Me.ButtonCca = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBoxSizeCod = New System.Windows.Forms.ComboBox()
        Me.Xl_AmtCost = New Winforms.Xl_Amount()
        Me.Xl_AmtTarifa = New Winforms.Xl_Amount()
        Me.Xl_PrNumero1 = New Winforms.Xl_PrNumero()
        Me.Xl_PrAdDoc1 = New Winforms.Xl_PrAdDoc()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "numero:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 454)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(707, 31)
        Me.Panel1.TabIndex = 48
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(488, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(599, 4)
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 177)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "página"
        '
        'TextBoxPagina
        '
        Me.TextBoxPagina.Location = New System.Drawing.Point(64, 174)
        Me.TextBoxPagina.Name = "TextBoxPagina"
        Me.TextBoxPagina.Size = New System.Drawing.Size(68, 20)
        Me.TextBoxPagina.TabIndex = 50
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 206)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "tarifa"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 232)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(22, 13)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "net"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(64, 264)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "ordre de compra"
        '
        'ButtonOrdreDeCompra
        '
        Me.ButtonOrdreDeCompra.Enabled = False
        Me.ButtonOrdreDeCompra.Location = New System.Drawing.Point(184, 260)
        Me.ButtonOrdreDeCompra.Name = "ButtonOrdreDeCompra"
        Me.ButtonOrdreDeCompra.Size = New System.Drawing.Size(30, 20)
        Me.ButtonOrdreDeCompra.TabIndex = 56
        Me.ButtonOrdreDeCompra.Text = "..."
        Me.ButtonOrdreDeCompra.UseVisualStyleBackColor = True
        '
        'ButtonCca
        '
        Me.ButtonCca.Enabled = False
        Me.ButtonCca.Location = New System.Drawing.Point(184, 286)
        Me.ButtonCca.Name = "ButtonCca"
        Me.ButtonCca.Size = New System.Drawing.Size(30, 20)
        Me.ButtonCca.TabIndex = 58
        Me.ButtonCca.Text = "..."
        Me.ButtonCca.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(64, 290)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "factura"
        '
        'ComboBoxSizeCod
        '
        Me.ComboBoxSizeCod.FormattingEnabled = True
        Me.ComboBoxSizeCod.Items.AddRange(New Object() {"(codi pagina)", "pagina sencera", "mitja pagina horitzontal", "mitja pagina vertical"})
        Me.ComboBoxSizeCod.Location = New System.Drawing.Point(64, 89)
        Me.ComboBoxSizeCod.Name = "ComboBoxSizeCod"
        Me.ComboBoxSizeCod.Size = New System.Drawing.Size(265, 21)
        Me.ComboBoxSizeCod.TabIndex = 59
        '
        'Xl_AmtCost
        '
        Me.Xl_AmtCost.Amt = Nothing
        Me.Xl_AmtCost.Location = New System.Drawing.Point(64, 227)
        Me.Xl_AmtCost.Name = "Xl_AmtCost"
        Me.Xl_AmtCost.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmtCost.TabIndex = 53
        '
        'Xl_AmtTarifa
        '
        Me.Xl_AmtTarifa.Amt = Nothing
        Me.Xl_AmtTarifa.Location = New System.Drawing.Point(64, 201)
        Me.Xl_AmtTarifa.Name = "Xl_AmtTarifa"
        Me.Xl_AmtTarifa.Size = New System.Drawing.Size(150, 20)
        Me.Xl_AmtTarifa.TabIndex = 51
        '
        'Xl_PrNumero1
        '
        Me.Xl_PrNumero1.Location = New System.Drawing.Point(64, 24)
        Me.Xl_PrNumero1.Name = "Xl_PrNumero1"
        Me.Xl_PrNumero1.Numero = Nothing
        Me.Xl_PrNumero1.Size = New System.Drawing.Size(265, 20)
        Me.Xl_PrNumero1.TabIndex = 0
        '
        'Xl_PrAdDoc1
        '
        Me.Xl_PrAdDoc1.AdDoc = Nothing
        Me.Xl_PrAdDoc1.Location = New System.Drawing.Point(64, 51)
        Me.Xl_PrAdDoc1.Name = "Xl_PrAdDoc1"
        Me.Xl_PrAdDoc1.Size = New System.Drawing.Size(265, 20)
        Me.Xl_PrAdDoc1.TabIndex = 60
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "anunci"
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(348, 13)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 62
        '
        'Frm_PrInsercio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(707, 485)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Xl_PrAdDoc1)
        Me.Controls.Add(Me.ComboBoxSizeCod)
        Me.Controls.Add(Me.ButtonCca)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ButtonOrdreDeCompra)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_AmtCost)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_AmtTarifa)
        Me.Controls.Add(Me.TextBoxPagina)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_PrNumero1)
        Me.Name = "Frm_PrInsercio"
        Me.Text = "INSERCIO PUBLICITARIA"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_PrNumero1 As Xl_PrNumero
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPagina As System.Windows.Forms.TextBox
    Friend WithEvents Xl_AmtTarifa As Xl_Amount
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCost As Xl_Amount
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ButtonOrdreDeCompra As System.Windows.Forms.Button
    Friend WithEvents ButtonCca As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxSizeCod As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_PrAdDoc1 As Xl_PrAdDoc
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Xl_DocFile1 As Winforms.Xl_DocFile
End Class
