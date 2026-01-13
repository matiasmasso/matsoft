
Public Class Frm_Escriptura
    Private _Escriptura As DTOEscriptura = Nothing
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oEscriptura As DTOEscriptura)
        MyBase.New()
        Me.InitializeComponent()
        LoadCodis()
        _Escriptura = oEscriptura
        BLL.BLLEscriptura.Load(_Escriptura)
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
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
            TextBoxNumProtocol.Text = .NumProtocol
            Xl_ContactRegistreMercantil.Contact = .RegistreMercantil
            TextBoxTomo.Text = .Tomo
            TextBoxFolio.Text = .Folio
            TextBoxHoja.Text = .Hoja
            TextBoxInscripcio.Text = .Inscripcio
            DateTimePickerFchFrom.Value = .FchFrom
            If .FchTo = Date.MinValue Or .FchTo.ToShortDateString = Date.MaxValue.ToShortDateString Then
                CheckBoxIndefinit.Checked = True
                DateTimePickerFchTo.Enabled = False
            Else
                DateTimePickerFchTo.Enabled = True
                DateTimePickerFchTo.Value = .FchTo
            End If
            Xl_DocFile1.Load(.DocFile)

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
    End Sub

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

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Escriptura
            .Codi = ComboBoxCodis.SelectedValue
            .Nom = TextBoxNom.Text
            .Obs = TextBoxObs.Text
            .Notari = Xl_ContactNotari.Contact
            .NumProtocol = TextBoxNumProtocol.Text
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxIndefinit.Checked Then
                .FchTo = Date.MaxValue
            Else
                .FchTo = DateTimePickerFchTo.Value
            End If
            .RegistreMercantil = Xl_ContactRegistreMercantil.Contact
            .Tomo = TextBoxTomo.Text
            .Folio = TextBoxFolio.Text
            .Hoja = TextBoxHoja.Text
            .Inscripcio = TextBoxInscripcio.Text
            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLEscriptura.Update(_Escriptura, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Escriptura))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If

    End Sub


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_ContactNotari.AfterUpdate, _
     ComboBoxCodis.SelectedIndexChanged, _
      TextBoxNom.TextChanged, _
      TextBoxObs.TextChanged, _
       DateTimePickerFchFrom.ValueChanged, _
        DateTimePickerFchTo.ValueChanged, _
         CheckBoxIndefinit.CheckedChanged, _
          TextBoxNumProtocol.TextChanged, _
           Xl_ContactRegistreMercantil.AfterUpdate, _
            TextBoxTomo.TextChanged, _
             TextBoxFolio.TextChanged, _
              TextBoxHoja.TextChanged, _
               TextBoxInscripcio.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta Escriptura?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLEscriptura.Delete(_Escriptura, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Escriptura))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la escriptura")
            End If
        End If
    End Sub

    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterFileDropped
        ButtonOk.Enabled = True
    End Sub
End Class