Public Class Frm_TrainingSession

    Private _TrainingSession As TrainingSession
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As TrainingSession)
        MyBase.New()
        Me.InitializeComponent()
        _TrainingSession = value
        TrainingSessionLoader.Load(_TrainingSession)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _TrainingSession
            DateTimePicker1.Value = .Fch
            Xl_ContactCustomer.Contact = .Customer
            TextBoxContact.Text = .ContactPerson
            Xl_ContactTrainer.Contact = .Trainer
            TextBoxObs.Text = .Obs
            Xl_TrainedPersons1.Load(.TrainedPersons)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxContact.TextChanged, _
         DateTimePicker1.ValueChanged, _
          Xl_ContactCustomer.AfterUpdate, _
           Xl_ContactTrainer.AfterUpdate, _
            TextBoxObs.TextChanged, _
             Xl_TrainedPersons1.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _TrainingSession
            .Fch = DateTimePicker1.Value
            .Customer = Xl_ContactCustomer.Contact
            .ContactPerson = TextBoxContact.Text
            .Trainer = Xl_ContactTrainer.Contact
            .Obs = TextBoxObs.Text
            .TrainedPersons = Xl_TrainedPersons1.Values
        End With

        Dim exs as New List(Of exception)
        If TrainingSessionLoader.Update(_TrainingSession, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_TrainingSession))
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If TrainingSessionloader.Delete(_TrainingSession, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_TrainingSession))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


