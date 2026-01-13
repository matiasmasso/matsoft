Public Class frm_NewSpvOut
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents BtnEnd As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnMore As System.Windows.Forms.Button
    Friend WithEvents TextBoxJob As System.Windows.Forms.TextBox
    Friend WithEvents LabelSpv As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSpv As System.Windows.Forms.TextBox
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
    Friend WithEvents TextBoxBts As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxKgs As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LabelCli As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonGSI As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonGNO As System.Windows.Forms.RadioButton

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents LabelTrabajosEfectuados As System.Windows.Forms.Label
    Friend WithEvents CheckBoxRecogeran As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxRef As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxM3 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LabelRef As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelTrabajosEfectuados = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxBts = New System.Windows.Forms.TextBox()
        Me.TextBoxKgs = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.LabelRef = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonGSI = New System.Windows.Forms.RadioButton()
        Me.RadioButtonGNO = New System.Windows.Forms.RadioButton()
        Me.TextBoxSpv = New System.Windows.Forms.TextBox()
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown()
        Me.LabelSpv = New System.Windows.Forms.Label()
        Me.TextBoxJob = New System.Windows.Forms.TextBox()
        Me.BtnMore = New System.Windows.Forms.Button()
        Me.BtnEnd = New System.Windows.Forms.Button()
        Me.LabelCli = New System.Windows.Forms.Label()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.CheckBoxRecogeran = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBoxM3 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(149, 448)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "&Kg:"
        '
        'DateTimePicker
        '
        Me.DateTimePicker.CustomFormat = ""
        Me.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker.Location = New System.Drawing.Point(8, 16)
        Me.DateTimePicker.Name = "DateTimePicker"
        Me.DateTimePicker.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePicker.TabIndex = 22
        Me.DateTimePicker.Value = New Date(2001, 11, 26, 19, 2, 43, 616)
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(352, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 16)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "nº"
        '
        'LabelTrabajosEfectuados
        '
        Me.LabelTrabajosEfectuados.Location = New System.Drawing.Point(8, 109)
        Me.LabelTrabajosEfectuados.Name = "LabelTrabajosEfectuados"
        Me.LabelTrabajosEfectuados.Size = New System.Drawing.Size(112, 16)
        Me.LabelTrabajosEfectuados.TabIndex = 17
        Me.LabelTrabajosEfectuados.Text = "trabajos efectuados:"
        Me.LabelTrabajosEfectuados.Visible = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(64, 448)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "&bultos:"
        '
        'TextBoxBts
        '
        Me.TextBoxBts.Location = New System.Drawing.Point(104, 448)
        Me.TextBoxBts.Name = "TextBoxBts"
        Me.TextBoxBts.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxBts.TabIndex = 8
        Me.TextBoxBts.Text = "0"
        '
        'TextBoxKgs
        '
        Me.TextBoxKgs.Location = New System.Drawing.Point(173, 448)
        Me.TextBoxKgs.Name = "TextBoxKgs"
        Me.TextBoxKgs.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxKgs.TabIndex = 9
        Me.TextBoxKgs.Text = "0"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBoxRef)
        Me.GroupBox1.Controls.Add(Me.LabelRef)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextBoxSpv)
        Me.GroupBox1.Controls.Add(Me.NumericUpDownYea)
        Me.GroupBox1.Controls.Add(Me.LabelSpv)
        Me.GroupBox1.Controls.Add(Me.LabelTrabajosEfectuados)
        Me.GroupBox1.Controls.Add(Me.TextBoxJob)
        Me.GroupBox1.Controls.Add(Me.BtnMore)
        Me.GroupBox1.Location = New System.Drawing.Point(56, 94)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(504, 330)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parte de reparación"
        '
        'TextBoxRef
        '
        Me.TextBoxRef.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxRef.Location = New System.Drawing.Point(232, 75)
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(264, 20)
        Me.TextBoxRef.TabIndex = 4
        Me.TextBoxRef.Visible = False
        '
        'LabelRef
        '
        Me.LabelRef.Location = New System.Drawing.Point(8, 75)
        Me.LabelRef.Name = "LabelRef"
        Me.LabelRef.Size = New System.Drawing.Size(216, 16)
        Me.LabelRef.TabIndex = 20
        Me.LabelRef.Text = "su referencia (num.pedido, resguardo...):"
        Me.LabelRef.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioButtonGSI)
        Me.GroupBox2.Controls.Add(Me.RadioButtonGNO)
        Me.GroupBox2.Location = New System.Drawing.Point(328, 32)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(168, 32)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Garantía"
        Me.GroupBox2.Visible = False
        '
        'RadioButtonGSI
        '
        Me.RadioButtonGSI.Location = New System.Drawing.Point(77, 8)
        Me.RadioButtonGSI.Name = "RadioButtonGSI"
        Me.RadioButtonGSI.Size = New System.Drawing.Size(40, 20)
        Me.RadioButtonGSI.TabIndex = 3
        Me.RadioButtonGSI.Text = "SI"
        '
        'RadioButtonGNO
        '
        Me.RadioButtonGNO.Location = New System.Drawing.Point(125, 8)
        Me.RadioButtonGNO.Name = "RadioButtonGNO"
        Me.RadioButtonGNO.Size = New System.Drawing.Size(40, 20)
        Me.RadioButtonGNO.TabIndex = 3
        Me.RadioButtonGNO.Text = "NO"
        '
        'TextBoxSpv
        '
        Me.TextBoxSpv.Location = New System.Drawing.Point(368, 8)
        Me.TextBoxSpv.Name = "TextBoxSpv"
        Me.TextBoxSpv.Size = New System.Drawing.Size(72, 20)
        Me.TextBoxSpv.TabIndex = 0
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Location = New System.Drawing.Point(440, 8)
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.NumericUpDownYea.Size = New System.Drawing.Size(56, 20)
        Me.NumericUpDownYea.TabIndex = 1
        Me.NumericUpDownYea.TabStop = False
        '
        'LabelSpv
        '
        Me.LabelSpv.Location = New System.Drawing.Point(8, 32)
        Me.LabelSpv.Name = "LabelSpv"
        Me.LabelSpv.Size = New System.Drawing.Size(360, 40)
        Me.LabelSpv.TabIndex = 15
        '
        'TextBoxJob
        '
        Me.TextBoxJob.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBoxJob.Location = New System.Drawing.Point(8, 140)
        Me.TextBoxJob.Multiline = True
        Me.TextBoxJob.Name = "TextBoxJob"
        Me.TextBoxJob.Size = New System.Drawing.Size(485, 145)
        Me.TextBoxJob.TabIndex = 6
        Me.TextBoxJob.Visible = False
        '
        'BtnMore
        '
        Me.BtnMore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnMore.Location = New System.Drawing.Point(241, 300)
        Me.BtnMore.Name = "BtnMore"
        Me.BtnMore.Size = New System.Drawing.Size(252, 24)
        Me.BtnMore.TabIndex = 7
        Me.BtnMore.Text = "&MAS REPARACIONES DEL MISMO CLIENTE"
        Me.BtnMore.Visible = False
        '
        'BtnEnd
        '
        Me.BtnEnd.Location = New System.Drawing.Point(464, 440)
        Me.BtnEnd.Name = "BtnEnd"
        Me.BtnEnd.Size = New System.Drawing.Size(88, 24)
        Me.BtnEnd.TabIndex = 11
        Me.BtnEnd.Text = "&FIN"
        '
        'LabelCli
        '
        Me.LabelCli.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCli.Location = New System.Drawing.Point(112, 16)
        Me.LabelCli.Name = "LabelCli"
        Me.LabelCli.Size = New System.Drawing.Size(373, 16)
        Me.LabelCli.TabIndex = 21
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(368, 440)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(88, 24)
        Me.ButtonCancel.TabIndex = 23
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'CheckBoxRecogeran
        '
        Me.CheckBoxRecogeran.Location = New System.Drawing.Point(67, 426)
        Me.CheckBoxRecogeran.Name = "CheckBoxRecogeran"
        Me.CheckBoxRecogeran.Size = New System.Drawing.Size(152, 16)
        Me.CheckBoxRecogeran.TabIndex = 24
        Me.CheckBoxRecogeran.TabStop = False
        Me.CheckBoxRecogeran.Text = "el cliente lo trae y recoge"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.SpvCli2.My.Resources.Resources.SpvOut
        Me.PictureBox1.Location = New System.Drawing.Point(512, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox1.TabIndex = 25
        Me.PictureBox1.TabStop = False
        '
        'TextBoxM3
        '
        Me.TextBoxM3.Location = New System.Drawing.Point(245, 448)
        Me.TextBoxM3.Name = "TextBoxM3"
        Me.TextBoxM3.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxM3.TabIndex = 10
        Me.TextBoxM3.Text = "0"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(221, 448)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "&m3:"
        '
        'frm_NewSpvOut
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(568, 485)
        Me.Controls.Add(Me.TextBoxM3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.CheckBoxRecogeran)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.DateTimePicker)
        Me.Controls.Add(Me.LabelCli)
        Me.Controls.Add(Me.TextBoxBts)
        Me.Controls.Add(Me.TextBoxKgs)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnEnd)
        Me.Name = "frm_NewSpvOut"
        Me.Text = "NOU ALBARA PER EL CLIENT"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _Spv As DTOSpv
    Private _SpvOut As DTOSpvOut
    Private _Spvs As List(Of DTOSpv)
    Private _Customer As DTOCustomer
    Private exc As Exception

    Private Sub frm_NewSpvOut_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DtFch As Date = Today
        Dim IntYea As Integer = Year(DtFch)
        With NumericUpDownYea
            .Minimum = 2001
            .Maximum = IntYea + 1
            .Value = IntYea
        End With
        DateTimePicker.Value = DtFch
        _Spvs = New List(Of DTOSpv)
    End Sub

    Private Async Sub BtnEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEnd.Click
        If LabelSpv.Text > "" Then
            If (RadioButtonGSI.Checked = False And RadioButtonGNO.Checked = False) Then
                MsgBox("Falta confirmar garantía")
                Exit Sub
            End If
            AddSpv() 'no hem picat BtnMore
        End If
        If _Spvs Is Nothing Then Throw New Exception("No hay ninguna reparación", exc)
        If TextBoxBts.Text = "" Then
            MsgBox("faltan bultos", MsgBoxStyle.Exclamation, "SERVICIO TECNICO")
            Exit Sub
        End If
        If TextBoxKgs.Text = "" Then
            MsgBox("faltan Kilos", MsgBoxStyle.Exclamation, "SERVICIO TECNICO")
            Exit Sub
        End If
        If TextBoxM3.Text = "" Then
            MsgBox("faltan el volumen en metros cubicos", MsgBoxStyle.Exclamation, "SERVICIO TECNICO")
            Exit Sub
        End If

        _SpvOut = DTOSpvOut.Factory(Current.Session.User, DateTimePicker.Value)
        With _SpvOut
            .Customer = _Customer
            .Spvs = _Spvs
            .Bts = TextBoxBts.Text
            .Kgs = TextBoxKgs.Text
            .M3 = TextBoxM3.Text
            .Recogeran = CheckBoxRecogeran.Checked
        End With

        Dim exs As New List(Of Exception)
        Dim oSpvOut = Await FEB2.SpvOut.Update(GlobalVariables.Emp, _SpvOut, exs)
        If exs.Count = 0 Then
            Dim oDelivery As DTODelivery = oSpvOut.Delivery ' PrxSpv.Delivery(oRefreshedSpvOut.DeliveryGuid)

            root.PrintDelivery(oDelivery)
            root.PrintDeliveryLabel(oDelivery)

            'MsgBox("Salida " & oRefreshedSpvOut.Delivery.Id)
            Me.Close()
            ClearForm()
            _Customer = New DTOCustomer
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub TextBoxSpv_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextBoxSpv.Validating
        If TextBoxSpv.Text = "" Then Exit Sub
        Try
            Dim year As Integer = NumericUpDownYea.Value
            Dim id As Integer = TextBoxSpv.Text

            Dim exs As New List(Of Exception)
            _Spv = FEB2.Spv.FromIdSync(GlobalVariables.Emp, year, id, exs)
            If exs.Count > 0 Then
                UIHelper.WarnError(exs)

                TextBoxSpv.Text = ""
                e.Cancel = True
            Else
                If _Spv Is Nothing Then
                    MsgBox("Numero no existente", MsgBoxStyle.Exclamation, "BERTRAN")
                    Me.Cursor = Cursors.Default
                    e.Cancel = True
                    Exit Sub
                End If

                If _Spv.SpvIn Is Nothing Then
                    MsgBox("Falta fer primer la entrada", MsgBoxStyle.Exclamation, "BERTRAN")
                    Me.Cursor = Cursors.Default
                    e.Cancel = True
                    Exit Sub
                End If

                If _Customer Is Nothing Then
                    _Customer = _Spv.Customer
                    LabelCli.Text = _Customer.FullNom
                Else
                    If _Customer.UnEquals(_Spv.Customer) Then Throw New Exception("Clientes distintos", exc)
                End If
            End If


        Catch exc As Exception
            MsgBox(exc.Message)
            e.Cancel = True
        End Try
    End Sub

    Private Sub TextBoxSpv_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxSpv.Validated
        If TextBoxSpv.Text = "" Then Exit Sub

        GroupBox2.Visible = True
        LabelRef.Visible = True
        TextBoxRef.Visible = True
        LabelTrabajosEfectuados.Visible = True
        TextBoxJob.Visible = True
        BtnMore.Visible = True

        LabelSpv.Text = _Spv.Customer.FullNom & vbCrLf & _Spv.Product.Nom
        TextBoxRef.Text = _Spv.sRef
    End Sub

    Private Sub BtnMore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMore.Click
        If (RadioButtonGSI.Checked = False And RadioButtonGNO.Checked = False) Then
            MsgBox("Falta confirmar garantía")
            Exit Sub
        End If

        AddSpv()
        ResetSpvControls()
        TextBoxSpv.Select()
    End Sub

    Private Sub ResetSpvControls()

        GroupBox2.Visible = False
        LabelRef.Visible = False
        TextBoxRef.Visible = False
        LabelTrabajosEfectuados.Visible = False
        TextBoxJob.Visible = False
        BtnMore.Visible = False

        TextBoxSpv.Text = ""
        LabelSpv.Text = ""
        RadioButtonGSI.Checked = False
        RadioButtonGNO.Checked = False
        TextBoxJob.Text = ""
    End Sub

    Public Sub ClearForm()
        ResetSpvControls()
        TextBoxBts.Text = ""
        TextBoxKgs.Text = ""
    End Sub

    Private Sub AddSpv()
        Dim i As Integer
        With _Spv
            .Garantia = (RadioButtonGSI.Checked = True)
            .sRef = TextBoxRef.Text
            .ObsTecnic = TextBoxJob.Text
        End With
        _Spvs.Add(_Spv)
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class
