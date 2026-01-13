Public Class Frm_Tracking

    Private _Tracking As DTOTracking
    Private _Cods As List(Of DTOCod)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOTracking)
        MyBase.New()
        Me.InitializeComponent()
        _Tracking = value
    End Sub

    Private Async Sub Frm_Tracking_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await LoadTrackingCodes()

        With _Tracking
            Me.Text = .Target.Nom
            DateTimePicker1.Value = .UsrLog.FchCreated
            Xl_LookupGuidnomUser.Load(.UsrLog.UsrCreated)
            ComboBoxTrackings.SelectedItem = _Cods.FirstOrDefault(Function(x) x.Guid.Equals(.Cod.Guid))
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Async Function LoadTrackingCodes() As Task
        Dim exs As New List(Of Exception)
        Dim oCodRoot = DTOCod.Wellknown(DTOCod.Wellknowns.Incidencias)
        _Cods = Await FEB.Cods.All(exs, oCodRoot)
        If exs.Count = 0 Then
            _Cods.Insert(0, New DTOCod(Guid.Empty))
            _Cods.First.Nom = DTOLangText.Factory("(seleccionar nou estat a afegir)")
            ComboBoxTrackings.DataSource = _Cods
            ComboBoxTrackings.DisplayMember = "DisplayMember"
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBox1.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Tracking
            .UsrLog.FchCreated = DateTimePicker1.Value
            .UsrLog.UsrCreated = Xl_LookupGuidnomUser.GuidNom
            .Cod = ComboBoxTrackings.SelectedItem
            .Obs = TextBox1.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.Tracking.Update(exs, _Tracking) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Tracking))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
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
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB.Tracking.Delete(exs, _Tracking) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Tracking))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


