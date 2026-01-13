

Public Class Frm_Quiz
    Private mNode As maxisrvr.TreeNodeObj
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oNode As maxisrvr.TreeNodeObj)
        MyBase.new()
        Me.InitializeComponent()
        mNode = oNode
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With CType(mNode.Obj, Quiz)
            TextBoxNom.Text = .Nom
            CheckBoxObsoleto.Checked = .Obsoleto
            If .FchCreated > Date.MinValue Then
                TextBoxFchCreated.Text = .FchCreated.ToShortDateString
            End If

            If .Exists Then
                ButtonDel.Enabled = .AllowDelete
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged, _
         CheckBoxObsoleto.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With CType(mNode.Obj, Quiz)
            .Nom = TextBoxNom.Text
            .Obsoleto = CheckBoxObsoleto.Checked
            .Update()
            RaiseEvent AfterUpdate(mNode, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim oQuiz As Quiz = CType(mNode.Obj, Quiz)
        If oQuiz.AllowDelete Then
            oQuiz.Delete()
            mNode.Obj = Nothing
            RaiseEvent AfterUpdate(mNode, System.EventArgs.Empty)
            Me.Close()
        End If
    End Sub
End Class