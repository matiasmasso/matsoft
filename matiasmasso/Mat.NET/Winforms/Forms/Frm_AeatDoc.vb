
Public Class Frm_AeatDoc
    Private _AeatDoc As DTOAeatDoc
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOAeatDoc)
        MyBase.New()
        Me.InitializeComponent()
        _AeatDoc = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.AeatDoc.Load(_AeatDoc, exs) Then
            With _AeatDoc
                TextBoxModel.Text = .Model.Nom
                DateTimePicker1.Value = .Fch
                NumericUpDownPeriod.Value = .Period
                Xl_DocFile1.Load(.DocFile)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        DateTimePicker1.ValueChanged,
         NumericUpDownPeriod.ValueChanged,
          Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        With _AeatDoc
            .Emp = Current.Session.Emp
            .Fch = DateTimePicker1.Value
            .Period = NumericUpDownPeriod.Value
            .DocFile = Xl_DocFile1.Value
        End With

        If Await FEB2.AeatDoc.Update(_AeatDoc, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_AeatDoc))
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
            If Await FEB2.AeatDoc.Delete(_AeatDoc, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_AeatDoc))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


