



Public Class Frm_QuizStep
    Private mNode As maxisrvr.TreeNodeObj
    Private mAllowEvents As Boolean = False
    Private mQuiz_Imgs As Quiz_Imgs
    Private mDirtyImages As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oNode As maxisrvr.TreeNodeObj)
        MyBase.new()
        Me.InitializeComponent()
        mNode = oNode
        'Me.Text = mObject.ToString
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With CType(mNode.Obj, Quiz_Step)
            TextBoxTitle.Text = .Title
            TextBoxText.Text = .Text
            mQuiz_Imgs = .Quiz.images
            For Each oItm As Quiz_Img In mQuiz_Imgs
                AddImage(oItm)
            Next

            If .Exists Then
                ButtonDel.Enabled = True
            End If
        End With
    End Sub



    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxTitle.TextChanged, _
         TextBoxText.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oStep As Quiz_Step = CType(mNode.Obj, Quiz_Step)
        With oStep
            .Title = TextBoxTitle.Text
            .Text = TextBoxText.Text
            .Update()

            If mDirtyImages Then
                Dim oQuiz As Quiz = oStep.Quiz
                oQuiz.images = GetImgsFromForm()
                oQuiz.Update()
            End If

            RaiseEvent AfterUpdate(mNode, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim oStep As Quiz_Step = CType(mNode.Obj, Quiz_Step)
        If oStep.AllowDelete Then
            oStep.Delete()
            Me.Close()
        End If
    End Sub


    Public Sub AddImage(oItem As Quiz_Img)
        ImageList1.Images.Add(oItem.Guid.ToString, oItem.Image)
        Dim idx As Integer = ImageList1.Images.Count
        Dim oListViewItem As New ListViewItem(oItem.Guid.ToString, oItem.Guid.ToString)
        ListView1.Items.Add(oListViewItem)
    End Sub



    Private Function GetImgsFromForm() As Quiz_Imgs
        Dim retval As New Quiz_Imgs
        Dim oStep As Quiz_Step = CType(mNode.Obj, Quiz_Step)
        Dim oItem As Quiz_Img = Nothing
        For i As Integer = 0 To ImageList1.Images.Count - 1
            Dim oGuid As Guid = GuidHelper.GetGuid(ListView1.Items(i).ImageKey)
            oItem = New Quiz_Img(oGuid)
            oItem.Image = mQuiz_Imgs(i).Image
            oItem.Quiz = oStep.Quiz
            retval.Add(oItem)
        Next
        Return retval
    End Function

    Private Sub ListView1_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles ListView1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub ListView1_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles ListView1.DragDrop

        Dim sFileNames() As String = Nothing
        Dim oStep As Quiz_Step = CType(mNode.Obj, Quiz_Step)
        Dim oQuiz As Quiz = oStep.Quiz

        'Try
        If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
            ' Check for the CTRL key. 
            If e.KeyState = 9 Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.Move
            End If

            sFileNames = e.Data.GetData(DataFormats.FileDrop)

            For Each sFilename As String In sFileNames
                Dim oImage As Image = Image.FromFile(sFilename)
                Dim oItm As New Quiz_Img(oQuiz, oImage)
                mQuiz_Imgs.Add(oItm)
                AddImage(oItm)
            Next

            mDirtyImages = True
            ButtonOk.Enabled = True
        Else
            MsgBox("format desconegut")
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub TextBoxText_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles TextBoxText.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub


    Private Sub ListView1_ItemDrag(sender As Object, e As System.Windows.Forms.ItemDragEventArgs) Handles ListView1.ItemDrag
        Dim oItem As ListViewItem = e.Item
        Dim sGuid As String = oItem.ImageKey
        DoDragDrop(sGuid, DragDropEffects.Copy)
    End Sub

    Private Sub TextBoxText_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles TextBoxText.DragOver
        If e.Data.GetDataPresent(DataFormats.StringFormat) Then
            ' Allow the drop.
            e.Effect = DragDropEffects.Copy

            ' Optionally move the cursor position so
            ' the user can see where the drop would happen.
            TextBoxText.Select(TextBoxCursorPos(TextBoxText, e.X, e.Y), 0)
        Else
            ' Do not allow the drop.
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Const EM_CHARFROMPOS As Int32 = &HD7
    Private Structure POINTAPI
        Public X As Integer
        Public Y As Integer
    End Structure

    Private Declare Function SendMessageLong Lib "user32" Alias _
        "SendMessageA" (ByVal hWnd As IntPtr, ByVal wMsg As  _
        Int32, ByVal wParam As Int32, ByVal lParam As Int32) As _
        Long

    ' Return the character position under the mouse.
    Public Function TextBoxCursorPos(ByVal txt As TextBox, _
        ByVal X As Single, ByVal Y As Single) As Long
        ' Convert screen coordinates into control coordinates.
        Dim pt As Point = TextBoxText.PointToClient(New Point(X, _
            Y))

        ' Get the character number
        TextBoxCursorPos = SendMessageLong(txt.Handle, _
            EM_CHARFROMPOS, 0&, CLng(pt.X + pt.Y * &H10000)) _
            And &HFFFF&
    End Function

    Private Sub TextBoxText_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) Handles TextBoxText.DragDrop
        Dim sGuid As String = e.Data.GetData(DataFormats.StringFormat)
        Dim sAnchor As String = "<img src='" & BLL.Defaults.GetImageUrl(DTO.Defaults.ImgTypes.QuizImg, New Guid(sGuid), True) & "' alt=''/>"
        TextBoxText.SelectedText = sAnchor

        ButtonOk.Enabled = True
    End Sub

    Private Sub ListView1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            Dim oItem As ListViewItem = ListView1.FocusedItem
            Dim sGuid As String = oItem.ImageKey

            If mQuiz_Imgs.RemoveByKey(sGuid) Then
                ListView1.Items.Remove(ListView1.FocusedItem)
                ImageList1.Images.RemoveByKey(sGuid)

                mDirtyImages = True
                ButtonOk.Enabled = True
            End If
        End If
    End Sub
End Class