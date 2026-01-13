Public Class Frm_AreaProvincia
    Private _AreaProvincia As DTOAreaProvincia
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOAreaProvincia)
        MyBase.New()
        Me.InitializeComponent()
        _AreaProvincia = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _AreaProvincia
            TextBoxCountry.Text = .Regio.Country.LangNom.Tradueix(Current.Session.Lang)
            Xl_LookupRegio1.AreaRegio = .Regio
            TextBoxNom.Text = .Nom
            TextBox347.Text = .Mod347
            TextBoxIntrastat.Text = .Intrastat
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
            Await refrescaZonas()
        End With
        _AllowEvent = True
    End Sub

    Private Async Function refrescaZonas() As Task
        Dim exs As New List(Of Exception)
        Dim oZonas = Await FEB.AreaProvincia.Zonas(_AreaProvincia, exs)
        If exs.Count = 0 Then
            Xl_Zonas1.Load(oZonas)
        Else
            UIHelper.WarnError("Error al carregar les zones:" & vbCrLf & ExceptionsHelper.ToFlatString(exs))
        End If
    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         Xl_LookupRegio1.AfterUpdate,
          TextBox347.TextChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _AreaProvincia
            .Regio = Xl_LookupRegio1.AreaRegio
            .nom = TextBoxNom.Text
            .Mod347 = TextBox347.Text
            .Intrastat = TextBoxIntrastat.Text
        End With
        Dim exs As New List(Of Exception)
        If Await FEB.AreaProvincia.Update(_AreaProvincia, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_AreaProvincia))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.AreaProvincia.Delete(_AreaProvincia, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_AreaProvincia))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub Xl_Zonas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Zonas1.RequestToRefresh
        Await refrescaZonas()
    End Sub
End Class


