

Public Class Frm_Quizs
    Private mCurrentNode As maxisrvr.TreeNodeObj

    Private Enum icons
        Quiz
        [Step]
        Question
        RightAnswer
        WrongAnswer
    End Enum

    Private Sub Frm_Quizs_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        refresca()
        SetContextMenu()
    End Sub

    Private Sub refresca()
        Dim oQuizs As Quizs = Quizs.All
        For Each oQuiz As Quiz In oQuizs
            Dim oNodeQuiz As New maxisrvr.TreeNodeObj(oQuiz.Nom, oQuiz, icons.Quiz, icons.Quiz)
            TreeView1.Nodes.Add(oNodeQuiz)
            For Each oStep As Quiz_Step In oQuiz.Steps
                Dim oNodeStep As New maxisrvr.TreeNodeObj(oStep.Title, oStep, icons.Step, icons.Step)
                oNodeQuiz.Nodes.Add(oNodeStep)
                For Each oQuestion As Quiz_Question In oStep.Questions
                    Dim oNodeQuestion As New maxisrvr.TreeNodeObj(oQuestion.Question, oQuestion, icons.Question, icons.Question)
                    oNodeStep.Nodes.Add(oNodeQuestion)
                    For Each oAnswer As Quiz_Answer In oQuestion.Answers
                        Dim oNodeAnswer As New maxisrvr.TreeNodeObj(oAnswer.Answer, oAnswer)
                        SetAnswerIcon(oNodeAnswer)
                        oNodeQuestion.Nodes.Add(oNodeAnswer)
                    Next
                Next
            Next
        Next

        If TreeView1.Nodes.Count > 0 Then
            TreeView1.Nodes(0).ExpandAll()
        End If

    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        'Dim oItm As Vehicle = CurrentItm()
        'If oItm IsNot Nothing Then
        ' oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom))
        'End If
        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewQuiz))
        TreeView1.ContextMenuStrip = oContextMenuStrip
    End Sub


    Private Sub RefreshRequest(sender As Object, e As System.EventArgs)
        Dim oNode As maxisrvr.TreeNodeObj = CType(sender, maxisrvr.TreeNodeObj)
        If oNode.Obj Is Nothing Then
            oNode.Remove()
        Else
            If TypeOf oNode.Obj Is Quiz Then
                oNode.Text = CType(oNode.Obj, Quiz).Nom
            ElseIf TypeOf oNode.Obj Is Quiz_Step Then
                oNode.Text = CType(oNode.Obj, Quiz_Step).Title
            ElseIf TypeOf oNode.Obj Is Quiz_Question Then
                oNode.Text = CType(oNode.Obj, Quiz_Question).Question
            ElseIf TypeOf oNode.Obj Is Quiz_Answer Then
                oNode.Text = CType(oNode.Obj, Quiz_Answer).Answer
            End If
        End If
    End Sub

    Private Sub TreeView1_AfterLabelEdit(sender As Object, e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView1.AfterLabelEdit
        TreeView1.LabelEdit = False
        If TypeOf mCurrentNode.Obj Is Quiz Then
            Dim oQuiz As Quiz = mCurrentNode.Obj
            oQuiz.Nom = e.Label
            oQuiz.Update()
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Step Then
            Dim oStep As Quiz_Step = mCurrentNode.Obj
            oStep.Title = e.Label
            oStep.Update()
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Question Then
            Dim oQuestion As Quiz_Question = mCurrentNode.Obj
            oQuestion.Question = e.Label
            oQuestion.Update()
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Answer Then
            Dim oAnswer As Quiz_Answer = mCurrentNode.Obj
            oAnswer.Answer = e.Label
            oAnswer.Update()
        End If
    End Sub

    Private Sub TreeView1_NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Dim oContextMenuStrip As New ContextMenuStrip
        mCurrentNode = e.Node
        If e.Node IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.clip, AddressOf Zoom))
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("editar", Nothing, AddressOf EditNodeLabel))
        End If
        If TypeOf mCurrentNode.Obj Is Quiz Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("usuaris", Nothing, AddressOf ShowUsrs))
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir step", Nothing, AddressOf AddNewChild))
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Step Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir questió", Nothing, AddressOf AddNewChild))
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Question Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir resposta", Nothing, AddressOf AddNewChild))
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("eliminar questió", Nothing, AddressOf DelNode))
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Answer Then
            Dim oAnswer As Quiz_Answer = CType(mCurrentNode.Obj, Quiz_Answer)
            Dim oMenuItemTrue As New ToolStripMenuItem("resposta verdadera", ImageList1.Images(icons.RightAnswer), AddressOf SetTrueAnswer)
            oMenuItemTrue.Enabled = Not oAnswer.Value
            oContextMenuStrip.Items.Add(oMenuItemTrue)
            Dim oMenuItemFalse As New ToolStripMenuItem("resposta erronia", ImageList1.Images(icons.WrongAnswer), AddressOf SetFalseAnswer)
            oMenuItemFalse.Enabled = oAnswer.Value
            oContextMenuStrip.Items.Add(oMenuItemFalse)

            Dim oMenuItemUp As New ToolStripMenuItem("pujar", My.Resources.GoUp, AddressOf GoUp)
            oMenuItemUp.Enabled = oAnswer.Ord > 1
            oContextMenuStrip.Items.Add(oMenuItemUp)
            Dim oMenuItemDown As New ToolStripMenuItem("baixar", My.Resources.GoDown, AddressOf GoDown)
            oMenuItemDown.Enabled = oAnswer.Ord < oAnswer.Question.Answers.Count
            oContextMenuStrip.Items.Add(oMenuItemDown)
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("eliminar resposta", Nothing, AddressOf DelNode))
        End If
        TreeView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub Zoom(sender As Object, e As System.EventArgs)
        If TypeOf mCurrentNode.Obj Is Quiz Then
            Dim oFrm As New Frm_Quiz(mCurrentNode)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Step Then
            Dim oStep As Quiz_Step = mCurrentNode.Obj
            Dim oFrm As New Frm_QuizStep(mCurrentNode)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Question Then
            Dim oQuestion As Quiz_Question = mCurrentNode.Obj
            'Dim oFrm As New Frm_QuizStep(oStep)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Answer Then
            Dim oAnswer As Quiz_Answer = mCurrentNode.Obj
            'Dim oFrm As New Frm_QuizStep(oStep)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()
        End If

    End Sub

    Private Sub ShowUsrs()
        Dim oFrm As New Frm_QuizUsrs
        oFrm.Show()
    End Sub

    Private Sub AddNewQuiz()
        Dim oQuiz As New Quiz
        oQuiz.Nom = "(nou tutorial)"
        oQuiz.Update()

        Dim oChildNode As New maxisrvr.TreeNodeObj(oQuiz.Nom, oQuiz)
        TreeView1.Nodes.Add(oChildNode)
    End Sub

    Private Sub AddNewChild(sender As Object, e As System.EventArgs)
        Dim oChildNode As maxisrvr.TreeNodeObj = Nothing

        If TypeOf mCurrentNode.Obj Is Quiz Then
            Dim oQuiz As Quiz = mCurrentNode.Obj
            Dim oStep As New Quiz_Step(oQuiz)
            oStep.Title = "(nou step)"
            oStep.Update()
            oChildNode = New maxisrvr.TreeNodeObj(oStep.Title, oStep, , icons.Step, icons.Step)
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Step Then
            Dim oStep As Quiz_Step = mCurrentNode.Obj
            Dim oQuestion As New Quiz_Question(oStep)
            oQuestion.Question = "(nova questió)"
            oQuestion.Update()
            oChildNode = New maxisrvr.TreeNodeObj(oQuestion.Question, oQuestion, , icons.Question, icons.Question)
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Question Then
            Dim oQuestion As Quiz_Question = mCurrentNode.Obj
            Dim oAnswer As New Quiz_Answer(oQuestion)
            oAnswer.Answer = "(nova resposta)"
            oAnswer.Update()
            oChildNode = New maxisrvr.TreeNodeObj(oAnswer.Answer, oAnswer, , icons.WrongAnswer, icons.WrongAnswer)
        End If

        mCurrentNode.Nodes.Add(oChildNode)
    End Sub

    Private Sub DelNode(sender As Object, e As System.EventArgs)
        Dim oChildNode As maxisrvr.TreeNodeObj = Nothing

        If TypeOf mCurrentNode.Obj Is Quiz Then
            Dim oQuiz As Quiz = mCurrentNode.Obj
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Step Then
            Dim oStep As Quiz_Step = mCurrentNode.Obj
            If oStep.AllowDelete Then
                oStep.Delete()
                mCurrentNode.Remove()
            End If
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Question Then
            Dim oQuestion As Quiz_Question = mCurrentNode.Obj
            If oQuestion.AllowDelete Then
                oQuestion.Delete()
                mCurrentNode.Remove()
            End If
        ElseIf TypeOf mCurrentNode.Obj Is Quiz_Answer Then
            Dim oAnswer As Quiz_Answer = mCurrentNode.Obj
            If oAnswer.AllowDelete Then
                oAnswer.Delete()
                mCurrentNode.Remove()
            End If
        End If

        'mCurrentNode.Nodes.Add(oChildNode)
    End Sub

    Private Sub EditNodeLabel(sender As Object, e As System.EventArgs)
        TreeView1.LabelEdit = True
    End Sub

    Private Sub SetTrueAnswer(sender As Object, e As System.EventArgs)
        Dim oNode As maxisrvr.TreeNodeObj = mCurrentNode
        SetAnswerValue(oNode, True)
    End Sub

    Private Sub SetFalseAnswer(sender As Object, e As System.EventArgs)
        Dim oNode As maxisrvr.TreeNodeObj = mCurrentNode
        SetAnswerValue(oNode, False)
    End Sub

    Private Sub SetAnswerValue(oNode As maxisrvr.TreeNodeObj, value As Boolean)
        Dim oAnswer As Quiz_Answer = CType(oNode.Obj, Quiz_Answer)
        oAnswer.Value = value
        oAnswer.Update()
        SetAnswerIcon(oNode)
    End Sub

    Private Sub SetAnswerIcon(oNode As maxisrvr.TreeNodeObj)
        Dim oAnswer As Quiz_Answer = CType(oNode.Obj, Quiz_Answer)
        oNode.ImageIndex = IIf(oAnswer.Value, icons.RightAnswer, icons.WrongAnswer)
        oNode.SelectedImageIndex = IIf(oAnswer.Value, icons.RightAnswer, icons.WrongAnswer)
    End Sub

    Private Sub DisplayAnswer(oQuestionNode As maxisrvr.TreeNodeObj, iAnswerOrd As Integer)
        Dim oAnswerNode As maxisrvr.TreeNodeObj = oQuestionNode.Nodes(iAnswerOrd - 1)
        Dim oAnswer As New Quiz_Answer(oQuestionNode.Obj, iAnswerOrd)
        oAnswerNode.Obj = oAnswer
        oAnswerNode.Text = oAnswer.Answer
        SetAnswerIcon(oAnswerNode)
    End Sub

    Private Sub GoUp(sender As Object, e As System.EventArgs)
        Dim oNode As maxisrvr.TreeNodeObj = mCurrentNode
        Dim oAnswer As Quiz_Answer = CType(oNode.Obj, Quiz_Answer)
        SwitchAnswer(oAnswer.Ord - 1)
    End Sub

    Private Sub GoDown(sender As Object, e As System.EventArgs)
        Dim oNode As maxisrvr.TreeNodeObj = mCurrentNode
        Dim oAnswer As Quiz_Answer = CType(oNode.Obj, Quiz_Answer)
        SwitchAnswer(oAnswer.Ord + 1)
    End Sub

    Private Sub SwitchAnswer(iSwitchTo As Integer)
        Dim oNode As maxisrvr.TreeNodeObj = mCurrentNode
        Dim oAnswer As Quiz_Answer = CType(oNode.Obj, Quiz_Answer)
        Dim iOrd As Integer = oAnswer.Ord
        oAnswer.SwitchWith(iSwitchTo)
        DisplayAnswer(oNode.Parent, iOrd)
        DisplayAnswer(oNode.Parent, iSwitchTo)
    End Sub
End Class