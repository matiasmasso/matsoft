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
            Me.Text = "Areas distribució " & .Brand.Nom
            Xl_LookupArea1.Area = .Area
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

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
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
        If BLL.BLLBrandArea.Update(_BrandArea, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BrandArea))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLBrandArea.Delete(_BrandArea, exs) Then
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
End Class


