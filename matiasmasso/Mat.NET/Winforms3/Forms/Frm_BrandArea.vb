Public Class Frm_BrandArea
    Private _BrandArea As DTOBrandArea
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBrandArea)
        MyBase.New()
        Me.InitializeComponent()
        _BrandArea = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _BrandArea
            Me.Text = "Areas distribució " & .Brand.nom.Tradueix(Current.Session.Lang)
            Xl_LookupArea1.Load(.Area)
            DateTimePickerFchFrom.Value = .FchFrom
            If .FchTo <> Nothing Then
                CheckBoxFchTo.Visible = True
                DateTimePickerFchTo.Value = .FchTo
            End If
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
       Xl_LookupArea1.AfterUpdate, _
        DateTimePickerFchFrom.ValueChanged, _
          DateTimePickerFchTo.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _BrandArea
            .Area = Xl_LookupArea1.Area
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
        End With
        Dim exs As New List(Of Exception)
        If Await FEB.BrandArea.Update(_BrandArea, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BrandArea))
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
            If Await FEB.BrandArea.Delete(_BrandArea, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BrandArea))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchTo.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchTo.Visible = CheckBoxFchTo.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_LookupArea1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupArea1.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectAny, Xl_LookupArea1.Area)
        AddHandler oFrm.onItemSelected, AddressOf onAreaSelected
        oFrm.Show()
    End Sub

    Private Sub onAreaSelected(sender As Object, e As MatEventArgs)
        Xl_LookupArea1.Load(e.Argument)
    End Sub
End Class


