Public Class Frm_BriqExam

    Private _BriqExam As BriqExam
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As BriqExam)
        MyBase.New()
        Me.InitializeComponent()
        _BriqExam = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        _AllowEvent = False
        With _BriqExam
            TextBoxTitle.Text = .Title
            DateTimePickerFchFrom.Value = .FchFrom
            If .FchTo IsNot Nothing Then
                CheckBoxCaducat.Checked = True
                DateTimePickerFchTo.Visible = True
                DateTimePickerFchTo.Value = .FchTo
            End If
            Xl_BriqSubjects1.Load(_BriqExam.Subjects)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxTitle.TextChanged, _
        DateTimePickerFchTo.ValueChanged, _
        DateTimePickerFchFrom.ValueChanged, _
        CheckBoxCaducat.CheckedChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        With _BriqExam
            .Title = TextBoxTitle.Text
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxCaducat.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
        End With

        If BriqExamloader.Update(_BriqExam, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BriqExam))
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
            If BriqExamloader.Delete(_BriqExam, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BriqExam))
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_BriqSubjects1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BriqSubjects1.RequestToAddNew
        Dim oSubject As New BriqSubject(_BriqExam)
        Dim oFrm As New Frm_BriqSubject(oSubject)
        AddHandler oFrm.AfterUpdate, AddressOf Reload
        oFrm.Show()
    End Sub

    Private Sub Reload()
        Dim oExams As BriqExams = BriqExamsLoader.All
        _BriqExam = oExams.Find(Function(x) x.Guid.Equals(_BriqExam.Guid))
        refresca()
    End Sub
End Class


