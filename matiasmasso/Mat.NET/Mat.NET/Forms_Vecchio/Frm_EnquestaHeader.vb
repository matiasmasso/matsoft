

Public Class Frm_EnquestaHeader
    Private mEnquestaHeader As EnquestaHeader
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oEnquestaHeader As EnquestaHeader)
        MyBase.new()
        Me.InitializeComponent()
        mEnquestaHeader = oEnquestaHeader
        If mEnquestaHeader.Exists Then
            Me.Text = "ENQUESTA " & mEnquestaHeader.Nom
        Else
            Me.Text = "NOVA ENQUESTA"
        End If
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mEnquestaHeader
            TextBoxNom.Text = .Nom
            DateTimePickerFchFrom.Value = .FchFrom
            DateTimePickerFchTo.Value = .FchTo
            If .Exists Then
                ButtonDel.Enabled = .AllowDelete
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged, _
         DateTimePickerFchFrom.ValueChanged, _
          DateTimePickerFchTo.ValueChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mEnquestaHeader
            .Nom = TextBoxNom.Text
            .FchFrom = DateTimePickerFchFrom.Value
            .FchTo = DateTimePickerFchTo.Value
            .Update()
            RaiseEvent AfterUpdate(mEnquestaHeader, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mEnquestaHeader.AllowDelete Then
            mEnquestaHeader.Delete()
            Me.Close()
        End If
    End Sub
End Class