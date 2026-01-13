

Public Class Frm_Evento
    Private mEvento As Evento
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oEvento As Evento)
        MyBase.New()
        Me.InitializeComponent()
        mEvento = oEvento
        Refresca()
        If oEvento.Id > 0 Then
            Me.Text = "NOU EVENT"
        Else
            Me.Text = "EVENT #" & oEvento.Id
        End If
        ButtonDel.Enabled = oEvento.AllowDelete
    End Sub

    Private Sub Refresca()
        With mEvento
            TextBoxEsp.Text = .Esp
            TextBoxCat.Text = .Cat
            TextBoxEng.Text = .Eng

            If .FchFrom > Date.MinValue Then
                DateTimePickerFrom.Value = .FchFrom
            End If

            If .FchTo > Date.MinValue Then
                DateTimePickerTo.Value = .FchTo
            End If

            mAllowEvents = True
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxEsp.TextChanged, _
    TextBoxCat.TextChanged, _
    TextBoxEng.TextChanged, _
    DateTimePickerFrom.ValueChanged, _
    DateTimePickerTo.ValueChanged

        ButtonOk.Enabled = True

    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mEvento
            .Esp = TextBoxEsp.Text
            .Cat = TextBoxCat.Text
            .Eng = TextBoxEng.Text
            .FchFrom = DateTimePickerFrom.Value
            .FchTo = DateTimePickerTo.Value
            .update()
        End With
        RaiseEvent AfterUpdate(mEvento, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class