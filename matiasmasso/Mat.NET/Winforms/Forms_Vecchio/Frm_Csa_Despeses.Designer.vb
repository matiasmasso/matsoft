Partial Public Class Frm_Csa_Despeses
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PictureBoxIBAN = New System.Windows.Forms.PictureBox()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.TextBoxCsa = New System.Windows.Forms.TextBox()
        Me.Xl_AmtInts = New Winforms.Xl_Amount()
        Me.Xl_AmtCurTotal = New Winforms.Xl_AmountCur()
        Me.Xl_AmtCom = New Winforms.Xl_Amount()
        Me.Xl_AmtIVA = New Winforms.Xl_Amount()
        Me.Xl_AmtCorreo = New Winforms.Xl_Amount()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxFra = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxCondicions = New System.Windows.Forms.TextBox()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile()
        CType(Me.PictureBoxIBAN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxIBAN
        '
        Me.PictureBoxIBAN.Location = New System.Drawing.Point(13, 12)
        Me.PictureBoxIBAN.Name = "PictureBoxIBAN"
        Me.PictureBoxIBAN.Size = New System.Drawing.Size(250, 50)
        Me.PictureBoxIBAN.TabIndex = 0
        Me.PictureBoxIBAN.TabStop = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(585, 440)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(91, 27)
        Me.ButtonOk.TabIndex = 10
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(8, 440)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(91, 27)
        Me.ButtonCancel.TabIndex = 11
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'TextBoxCsa
        '
        Me.TextBoxCsa.Location = New System.Drawing.Point(13, 69)
        Me.TextBoxCsa.Name = "TextBoxCsa"
        Me.TextBoxCsa.Size = New System.Drawing.Size(250, 20)
        Me.TextBoxCsa.TabIndex = 1
        Me.TextBoxCsa.TabStop = False
        '
        'Xl_AmtInts
        '
        Me.Xl_AmtInts.Amt = Nothing
        Me.Xl_AmtInts.Location = New System.Drawing.Point(135, 216)
        Me.Xl_AmtInts.Name = "Xl_AmtInts"
        Me.Xl_AmtInts.Size = New System.Drawing.Size(128, 20)
        Me.Xl_AmtInts.TabIndex = 5
        '
        'Xl_AmtCurTotal
        '
        Me.Xl_AmtCurTotal.Amt = Nothing
        Me.Xl_AmtCurTotal.Location = New System.Drawing.Point(135, 324)
        Me.Xl_AmtCurTotal.Name = "Xl_AmtCurTotal"
        Me.Xl_AmtCurTotal.Size = New System.Drawing.Size(160, 20)
        Me.Xl_AmtCurTotal.TabIndex = 9
        Me.Xl_AmtCurTotal.TabStop = False
        '
        'Xl_AmtCom
        '
        Me.Xl_AmtCom.Amt = Nothing
        Me.Xl_AmtCom.Location = New System.Drawing.Point(135, 243)
        Me.Xl_AmtCom.Name = "Xl_AmtCom"
        Me.Xl_AmtCom.Size = New System.Drawing.Size(128, 20)
        Me.Xl_AmtCom.TabIndex = 6
        '
        'Xl_AmtIVA
        '
        Me.Xl_AmtIVA.Amt = Nothing
        Me.Xl_AmtIVA.Location = New System.Drawing.Point(135, 270)
        Me.Xl_AmtIVA.Name = "Xl_AmtIVA"
        Me.Xl_AmtIVA.Size = New System.Drawing.Size(128, 20)
        Me.Xl_AmtIVA.TabIndex = 7
        '
        'Xl_AmtCorreo
        '
        Me.Xl_AmtCorreo.Amt = Nothing
        Me.Xl_AmtCorreo.Location = New System.Drawing.Point(135, 297)
        Me.Xl_AmtCorreo.Name = "Xl_AmtCorreo"
        Me.Xl_AmtCorreo.Size = New System.Drawing.Size(128, 20)
        Me.Xl_AmtCorreo.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(50, 218)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "interesos"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(50, 243)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "comissions"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(50, 270)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "IVA"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(50, 297)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "correo"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(50, 324)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Total"
        '
        'TextBoxFra
        '
        Me.TextBoxFra.Location = New System.Drawing.Point(172, 136)
        Me.TextBoxFra.Name = "TextBoxFra"
        Me.TextBoxFra.Size = New System.Drawing.Size(91, 20)
        Me.TextBoxFra.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(52, 137)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "factura nº"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(174, 163)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(89, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(52, 163)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(28, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "data"
        '
        'TextBoxCondicions
        '
        Me.TextBoxCondicions.Location = New System.Drawing.Point(13, 96)
        Me.TextBoxCondicions.Name = "TextBoxCondicions"
        Me.TextBoxCondicions.Size = New System.Drawing.Size(250, 20)
        Me.TextBoxCondicions.TabIndex = 2
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(326, 12)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 31
        '
        'Frm_Csa_Despeses
        '
        Me.ClientSize = New System.Drawing.Size(679, 470)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.TextBoxCondicions)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxFra)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_AmtCorreo)
        Me.Controls.Add(Me.Xl_AmtIVA)
        Me.Controls.Add(Me.Xl_AmtCom)
        Me.Controls.Add(Me.Xl_AmtCurTotal)
        Me.Controls.Add(Me.Xl_AmtInts)
        Me.Controls.Add(Me.TextBoxCsa)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.PictureBoxIBAN)
        Me.Name = "Frm_Csa_Despeses"
        Me.Text = "DESPESES REMESA AL DESCOMPTE"
        CType(Me.PictureBoxIBAN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxIBAN As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents TextBoxCsa As System.Windows.Forms.TextBox
    Friend WithEvents Xl_AmtInts As Xl_Amount
    Friend WithEvents Xl_AmtCurTotal As Xl_AmountCur
    Friend WithEvents Xl_AmtCom As Xl_Amount
    Friend WithEvents Xl_AmtIVA As Xl_Amount
    Friend WithEvents Xl_AmtCorreo As Xl_Amount
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFra As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCondicions As System.Windows.Forms.TextBox
    Friend WithEvents Xl_DocFile1 As Xl_DocFile
End Class
