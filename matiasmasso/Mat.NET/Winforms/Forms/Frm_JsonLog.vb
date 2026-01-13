Imports Newtonsoft.Json.Linq

Public Class Frm_JsonLog
    Private _JsonLog As DTOJsonLog
    Private _foundNodes As List(Of TreeNode)
    Private _foundIdx As Integer
    Private _AllowEvents As Boolean


    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOJsonLog)
        MyBase.New()
        Me.InitializeComponent()
        _JsonLog = value
    End Sub

    Private Sub Frm_JsonLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.JsonLog.Load(exs, _JsonLog) Then
            Me.Text = String.Format("JsonLog {0:dd/MM/yy HH:mm}", _JsonLog.FchCreated)
            refresca()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub refresca()
        With _JsonLog
            Dim obj As JObject = JObject.Parse(.Json)
            TreeView1.Nodes.Clear()
            Dim parent As TreeNode = Json2Tree(obj)
            parent.Text = "Root Object"
            TreeView1.Nodes.Add(parent)

            ButtonDel.Enabled = Not .IsNew
        End With
    End Sub

    Private Function Json2Tree(ByVal obj As JObject) As TreeNode
        Dim parent As TreeNode = New TreeNode()

        For Each token In obj
            parent.Text = token.Key.ToString()
            Dim child As TreeNode = New TreeNode()
            child.Text = token.Key.ToString()

            If token.Value.Type.ToString() = "Object" Then
                Dim o As JObject = CType(token.Value, JObject)
                child = Json2Tree(o)
                parent.Nodes.Add(child)
            ElseIf token.Value.Type.ToString() = "Array" Then
                Dim ix As Integer = -1

                For Each itm In token.Value

                    If itm.Type.ToString() = "Object" Then
                        Dim objTN As TreeNode = New TreeNode()
                        ix += 1
                        Dim o As JObject = CType(itm, JObject)
                        objTN = Json2Tree(o)
                        objTN.Text = token.Key.ToString() & "[" & ix & "]"
                        child.Nodes.Add(objTN)
                    ElseIf itm.Type.ToString() = "Array" Then
                        ix += 1
                        Dim dataArray As TreeNode = New TreeNode()

                        For Each oData In itm
                            dataArray.Text = token.Key.ToString() & "[" + ix & "]"
                            dataArray.Nodes.Add(oData.ToString())
                        Next

                        child.Nodes.Add(dataArray)
                    Else
                        child.Nodes.Add(itm.ToString())
                    End If
                Next

                parent.Nodes.Add(child)
            Else

                If token.Value.ToString() = "" Then
                    child.Nodes.Add("N/A")
                Else
                    child.Nodes.Add(token.Value.ToString())
                End If

                parent.Nodes.Add(child)
            End If
        Next

        Return parent
    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs)
        If _AllowEvents Then
            'ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB2.JsonLog.Delete(exs, _JsonLog) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_JsonLog))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CopiarGuidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopiarGuidToolStripMenuItem.Click
        UIHelper.CopyToClipboard(_JsonLog.Guid.ToString)
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Dim searchKey = e.Argument
        _foundNodes = New List(Of TreeNode)
        _foundIdx = 0
        search(TreeView1.Nodes(0), searchKey)
        ShowFoundNode(0)
    End Sub

    Private Sub search(oParentNode As TreeNode, searchKey As String)
        For Each oNode As TreeNode In oParentNode.Nodes
            If oNode.Text.Contains(searchKey) Then
                _foundNodes.Add(oNode)
            End If
            search(oNode, searchKey)
        Next
    End Sub

    Private Sub ButtonFirst_Click(sender As Object, e As EventArgs) Handles ButtonFirst.Click
        ShowFoundNode(0)
    End Sub

    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        ShowFoundNode(_foundIdx + 1)
    End Sub

    Private Sub ButtonPrevious_Click(sender As Object, e As EventArgs) Handles ButtonPrevious.Click
        ShowFoundNode(_foundIdx - 1)
    End Sub

    Private Sub ButtonLast_Click(sender As Object, e As EventArgs) Handles ButtonLast.Click
        ShowFoundNode(_foundNodes.Count - 1)
    End Sub
    Private Sub ShowFoundNode(foundIdx As Integer)
        _foundIdx = foundIdx


        If _foundNodes.Count = 0 Then
            LabelFound.Text = String.Format("{0} de {1}", 0, _foundNodes.Count)
            ButtonFirst.Enabled = False
            ButtonPrevious.Enabled = False
            ButtonLast.Enabled = False
            ButtonNext.Enabled = False
        Else
            _foundNodes(_foundIdx).EnsureVisible()
            LabelFound.Text = String.Format("{0} de {1}", _foundIdx + 1, _foundNodes.Count)

            ButtonFirst.Enabled = True
            ButtonPrevious.Enabled = True
            ButtonLast.Enabled = True
            ButtonNext.Enabled = True

            If _foundIdx = 0 Then
                ButtonFirst.Enabled = False
                ButtonPrevious.Enabled = False
            End If
            If _foundIdx = _foundNodes.Count - 1 Then
                ButtonLast.Enabled = False
                ButtonNext.Enabled = False
            End If
        End If


    End Sub

    Private Async Sub ProcesaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProcesaToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        'Dim oSerializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim oJsonLog = (New System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize(Of Vivace.ShipmentsReport)(oLog.Json)
        ProgressBar1.Visible = True

        Dim url As String = "http://localhost:55836/api/jsonlog/mailbox"
        Dim jsonInput = _JsonLog.Json
        Dim stringContent As New System.Net.Http.StringContent(jsonInput, System.Text.Encoding.UTF8, "application/json")

        Try
            Using client As New Net.Http.HttpClient
                Using response As Net.Http.HttpResponseMessage = Await client.PostAsync(url, stringContent)
                    ProgressBar1.Visible = False
                    If response.IsSuccessStatusCode Then
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(_JsonLog))
                        Me.Close()
                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        exs.Add(New Exception(errMsg))
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
            UIHelper.WarnError(ex)
        Finally
            If exs.Count = 0 Then
                MsgBox("Success", MsgBoxStyle.Information)
            Else
                UIHelper.WarnError(exs)
            End If
        End Try

    End Sub


    'Private Function SaveFile(exs As List(Of Exception), oFormatting As Formatting) As Boolean
    ' Dim retval As Boolean
    ' Dim oDlg As New SaveFileDialog
    ' With oDlg
    ' .AddExtension = True
    ' .DefaultExt = ".json"
    ' .Title = "Desar fitxer Json"
    ' .FileName = "Jsonlog " & _JsonLog.Guid.ToString & ".json"
    ' If .ShowDialog Then
    ' Dim oJObject As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(_JsonLog.Json)
    ' Dim formatted As String = Newtonsoft.Json.JsonConvert.SerializeObject(oJObject, oFormatting)
    '             retval = MatHelperStd.FileSystemHelper.SaveTextToFile(formatted, .FileName, exs)
    ' End If
    ' End With
    ' Return retval
    '' End Function

    Private Sub ExportFormattedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportFormattedToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        ' If SaveFile(exs, Formatting.Indented) Then
        'MsgBox("fitxer desat satisfactoriament", MsgBoxStyle.Information)
        'Else
        'If exs.Count > 0 Then UIHelper.WarnError(exs)
        'End If
    End Sub

    Private Sub ExportNoFormatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportNoFormatToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        'If SaveFile(exs, Formatting.None) Then
        ' MsgBox("fitxer desat satisfactoriament", MsgBoxStyle.Information)
        'Else
        ' If exs.Count > 0 Then UIHelper.WarnError(exs)
        'End If
    End Sub

    Private Sub ImportarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar fitxer Json"
            .Filter = "fitxers Json|*.json|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim src = FileSystemHelper.GetStringContentFromFile(.FileName)
                _JsonLog.Json = src
                refresca()
                ButtonOk.Enabled = True
            End If
        End With
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.JsonLog.Update(exs, _JsonLog) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_JsonLog))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class


