Public Class Frm_BriqSubject

    Private _BriqSubject As BriqSubject
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As BriqSubject)
        MyBase.New()
        Me.InitializeComponent()
        _BriqSubject = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        With _BriqSubject
            TextBoxExam.Text = .Exam.Title
            TextBoxTitle.Text = .Title
            TextBoxExcerpt.Text = .Excerpt
            Xl_BriqQuestionGroups1.Load(_BriqSubject.QuestionGroups)
            If Xl_BriqQuestionGroups1.Value IsNot Nothing Then
                Xl_BriqQuestions1.Load(Xl_BriqQuestionGroups1.Value.Questions)
            End If
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxTitle.TextChanged, _
         TextBoxExcerpt.TextChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        With _BriqSubject
            .Title = TextBoxTitle.Text
            .Excerpt = TextBoxExcerpt.Text
        End With

        If BriqSubjectloader.Update(_BriqSubject, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BriqSubject))
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
            If BriqSubjectloader.Delete(_BriqSubject, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BriqSubject))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub


    Private Sub RefreshRequest()
        Dim oExams As BriqExams = BriqExamsLoader.All
        Dim oExam As BriqExam = oExams.Find(Function(x) x.Guid.Equals(_BriqSubject.Exam.Guid))
        _BriqSubject = oExam.Subjects.Find(Function(x) x.Guid.Equals(_BriqSubject.Guid))
        refresca()
        RaiseEvent AfterUpdate(Me, New MatEventArgs(oExams))
    End Sub

    Private Sub Xl_BriqQuestionGroups1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BriqQuestionGroups1.RequestToAddNew
        Dim oNewGroup As New BriqQuestionGroup(_BriqSubject)
        Dim oFrm As New Frm_BriqQuestionGroup(oNewGroup)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Xl_BriqQuestionGroups1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_BriqQuestionGroups1.ValueChanged
        If _AllowEvent Then
            Dim oGroup As BriqQuestionGroup = e.Argument
            Xl_BriqQuestions1.Load(oGroup.Questions)
        End If
    End Sub

    Private Sub Xl_BriqQuestions1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BriqQuestions1.RequestToAddNew
        Dim oGroup As BriqQuestionGroup = Xl_BriqQuestionGroups1.Value
        If oGroup IsNot Nothing Then
            Dim oQuestion As New BriqQuestion(oGroup)
            Dim oFrm As New Frm_BriqQuestion(oQuestion)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If

    End Sub
End Class


