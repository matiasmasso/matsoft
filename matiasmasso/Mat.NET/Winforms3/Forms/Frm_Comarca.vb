Public Class Frm_Comarca

    Private _Comarca As DTOComarca
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOComarca)
        MyBase.New()
        Me.InitializeComponent()
        _Comarca = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Comarca.Load(_Comarca, exs) Then
            With _Comarca
                TextBox1.Text = _Comarca.Nom
                Xl_LookupArea1.Load(_Comarca.Zona)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBox1.TextChanged,
         Xl_LookupArea1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Comarca
            .nom = TextBox1.Text
            .Zona = Xl_LookupArea1.Area
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.Comarca.Update(_Comarca, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Comarca))
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
            If Await FEB.Comarca.Delete(_Comarca, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Comarca))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_LookupArea1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupArea1.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZona, Xl_LookupArea1.Area)
        AddHandler oFrm.onItemSelected, AddressOf onAreaSelected
        oFrm.Show()
    End Sub

    Private Sub onAreaSelected(sender As Object, e As MatEventArgs)
        Xl_LookupArea1.Load(e.Argument)
    End Sub
End Class


