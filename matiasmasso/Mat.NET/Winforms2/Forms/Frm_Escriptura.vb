
Public Class Frm_Escriptura
    Private _Escriptura As DTOEscriptura = Nothing
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oEscriptura As DTOEscriptura)
        MyBase.New()
        Me.InitializeComponent()
        LoadCodis()
        _Escriptura = oEscriptura
    End Sub

    Private Async Sub Frm_Escriptura_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        FEB.Escriptura.Load(_Escriptura, exs)
        If exs.Count = 0 Then
            Await Refresca()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function Refresca() As Task
        With _Escriptura
            If .IsNew Then
                Me.Text = "Nova Escriptura"
                ButtonDel.Enabled = False
            Else
                Me.Text = "Escriptura " & .Guid.ToString
                ButtonDel.Enabled = True
            End If

            ComboBoxCodis.SelectedValue = .Codi
            If .Notari IsNot Nothing Then
                Xl_ContactNotari.Contact = .Notari
            End If
            TextBoxNom.Text = .Nom
            TextBoxObs.Text = .Obs
            NumericUpDownProtocol.Value = .NumProtocol
            Xl_ContactRegistreMercantil.Contact = .RegistreMercantil
            NumericUpDownTomo.Text = .Tomo
            NumericUpDownFolio.Text = .Folio
            TextBoxHoja.Text = .Hoja
            NumericUpDownInscripcio.Value = .Inscripcio
            DateTimePickerFchFrom.Value = .FchFrom
            If .FchTo = Date.MinValue Or .FchTo.ToShortDateString = Date.MaxValue.ToShortDateString Then
                CheckBoxIndefinit.Checked = True
                DateTimePickerFchTo.Enabled = False
            Else
                DateTimePickerFchTo.Enabled = True
                DateTimePickerFchTo.Value = .FchTo
            End If
            Await Xl_DocFile1.Load(.DocFile)

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
    End Function

    Private Sub LoadCodis()
        UIHelper.LoadComboFromEnum(ComboBoxCodis, GetType(DTOEscriptura.Codis))
    End Sub

    Private Sub CheckBoxIndefinit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxIndefinit.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchTo.Enabled = Not CheckBoxIndefinit.Checked
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Escriptura
            .codi = ComboBoxCodis.SelectedValue
            .nom = TextBoxNom.Text
            .obs = TextBoxObs.Text
            .notari = Xl_ContactNotari.Contact
            .NumProtocol = NumericUpDownProtocol.Value
            .fchFrom = DateTimePickerFchFrom.Value
            If CheckBoxIndefinit.Checked Then
                .fchTo = Date.MaxValue
            Else
                .fchTo = DateTimePickerFchTo.Value
            End If
            .registreMercantil = Xl_ContactRegistreMercantil.Contact
            .Tomo = NumericUpDownTomo.Text
            .Folio = NumericUpDownFolio.Text
            .hoja = TextBoxHoja.Text
            .Inscripcio = NumericUpDownInscripcio.Value
            .docFile = Xl_DocFile1.Value
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.Escriptura.Update(_Escriptura, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Escriptura))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar la escriptura")
        End If

    End Sub


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_ContactNotari.AfterUpdate,
     ComboBoxCodis.SelectedIndexChanged,
      TextBoxNom.TextChanged,
      TextBoxObs.TextChanged,
       DateTimePickerFchFrom.ValueChanged,
        DateTimePickerFchTo.ValueChanged,
         CheckBoxIndefinit.CheckedChanged,
          NumericUpDownProtocol.ValueChanged,
          NumericUpDownFolio.ValueChanged,
           NumericUpDownInscripcio.ValueChanged,
            NumericUpDownTomo.ValueChanged,
           Xl_ContactRegistreMercantil.AfterUpdate,
              TextBoxHoja.TextChanged,
                Xl_DocFile1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta Escriptura?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            If Await FEB.Escriptura.Delete(_Escriptura, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Escriptura))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la escriptura")
            End If
        End If
    End Sub



End Class