Public Class Frm_QuizAdvansafix

    Private _QuizAdvansafix As DTOQuizAdvansafix
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOQuizAdvansafix)
        MyBase.New()
        Me.InitializeComponent()
        _QuizAdvansafix = value
        BLL.BLLQuizAdvansafix.Load(_QuizAdvansafix)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _QuizAdvansafix
            Xl_Contact21.Contact = .Customer
            NumericUpDownNoSICTPurchased.Value = .NoSICTPurchased
            NumericUpDownQtyNoSICT.Value = .QtyNoSICT
            NumericUpDownSICTPurchased.Value = .SICTPurchased
            NumericUpDownQtySICT.Value = .QtySICT
            If .LastUser IsNot Nothing Then
                TextBoxLastUser.Text = .LastUser.EmailAddress
            End If
            CheckBoxSplioOpen.Checked = .SplioOpen
            If .FchBrowse <> Nothing Then
                TextBoxClicked.Text = Format(.FchBrowse, "dd/MM/yy HH:mm")
            End If
            If .FchConfirmed <> Nothing Then
                TextBoxConfirmed.Text = Format(.FchConfirmed, "dd/MM/yy HH:mm")
            End If
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
             NumericUpDownQtyNoSICT.ValueChanged,
              NumericUpDownQtySICT.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _QuizAdvansafix
            .QtyNoSICT = NumericUpDownQtyNoSICT.Value
            .QtySICT = NumericUpDownQtySICT.Value
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLQuizAdvansafix.Update(_QuizAdvansafix, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_QuizAdvansafix))
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
            If BLL.BLLQuizAdvansafix.Delete(_QuizAdvansafix, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_QuizAdvansafix))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


