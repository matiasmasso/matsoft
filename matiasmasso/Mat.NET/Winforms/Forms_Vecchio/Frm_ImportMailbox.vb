
Imports Microsoft.Office.Interop

Public Class Frm_ImportMailbox

    Private mEmp As DTOEmp = Current.session.emp
    Private mDs As DataSet
    Private mDirtyErrs As Boolean

    Private Enum Cols
        Ico
        Result
        Source
        Txt
    End Enum

    Private Enum Results
        _NotSet
        Info
        Warn
        Err
        Omit
    End Enum

    Private _Filename As String
    Private _SkipMailBox As Boolean


    Public Sub New(Optional sFilename As String = "")
        MyBase.New
        InitializeComponent()
        _Filename = sFilename
        _SkipMailBox = True
    End Sub

    Public WriteOnly Property FileName() As String
        Set(ByVal Value As String)
        End Set
    End Property


    Private Sub Frm_ImportMailbox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim exs As New List(Of Exception)
        Cursor = Cursors.WaitCursor
        'Try
        If _SkipMailBox Then
            CreateDatatable()
            Me.Show()
            ImportXMLFile(exs, _Filename)
        Else
            CreateDatatable()
            Me.Show()
            'Application.DoEvents()
            ImportMailBoxAttachments(exs)
        End If


        'Catch ex As Exception
        'mDirtyErrs = True
        'Log(Results.Err, "GLOBAL", ex.Message)

        'Finally
        Cursor = Cursors.Default
        If mDirtyErrs Then
            Log(Results.Warn, "FI", "Importació finalitzad amb errors")
        Else
            Log(Results.Info, "FI", "Importació finalitzada correctament")

            'MsgBox("Importació finalitzada correctament", MsgBoxStyle.Information, "MAT.NET")
            'Me.Close()
        End If
        'End Try
    End Sub

    Public Function ImportMailBoxAttachments(exs As List(Of Exception)) As Boolean
        Dim oNameSpace = OutlookHelper.OutlookNameSpace(exs)
        If exs.Count = 0 Then
            Dim oInbox As Outlook.MAPIFolder = oNameSpace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox)
            Dim oObjItemsCollection As Object = oInbox.Items


            With ProgressBar1
                .Maximum = oInbox.Items.Count
                .Minimum = 1
                .Value = 1
            End With


            Dim oObj As Object
            Dim oMsg As Outlook.MailItem
            For Each oObj In oObjItemsCollection
                If TypeOf oObj Is Outlook.MailItem Then
                    oMsg = DirectCast(oObj, Outlook.MailItem)
                    Dim s As String = oMsg.ReceivedTime.ToShortDateString & " " & oMsg.Subject
                    ImportMailBoxAttachment(oMsg, exs)
                End If
                'Catch ex2 As InvalidCastException
                'MsgBox("halt! invalid cast exception")
                'Catch ex As Exception
                'Log(Results.Err, "MSG", oMsg.SenderName & ": " & oMsg.Subject)
                'Finally
                ProgressBar1.Increment(1)
                'End Try
            Next oObj
        Else
            UIHelper.WarnError(exs)
        End If
        Return exs.Count = 0
    End Function

    Public Function ImportMailBoxAttachment(ByVal oMsg As Outlook.MailItem, exs As List(Of Exception)) As Boolean
        Dim oAttach As Outlook.Attachment
        Dim oResult As Results

        'Try

        'If oMsg.SenderName = "Eva Macayo" Then Stop
        Select Case oMsg.SenderName
            Case "Universal Currency Converter"
                'If UpdateCurrencyRates(oMsg.Body) = Results.Info Then
                'oMsg.Delete()
                'End If

            Case Else
                oResult = Results._NotSet
                For Each oAttach In oMsg.Attachments
                    If IsXML(oAttach.FileName) Then
                        Dim result = ImportXMLAttachment(oAttach, exs)
                        Select Case result
                            Case Results.Info
                                If oResult = Results._NotSet Then
                                    oResult = Results.Info
                                End If
                            Case Results.Err
                                oResult = Results.Warn
                            Case Else
                                oResult = Results.Omit
                        End Select
                    End If
                Next oAttach

                Select Case oResult
                    Case Results.Info
                        Log(oResult, "", oMsg.ReceivedTime.ToShortDateString & " " & oMsg.SenderName & ": " & oMsg.Subject)
                        oMsg.Delete()
                    Case Results.Warn
                        Log(oResult, "", oMsg.ReceivedTime.ToShortDateString & " " & oMsg.SenderName & ": " & oMsg.Subject)
                    Case Results.Omit
                End Select
        End Select
        Return exs.Count = 0
    End Function

    Private Function IsXML(ByVal sFileName As String) As Boolean
        Dim retval As Boolean
        Dim iLen As Integer = sFileName.Length
        Dim sExtension As String = sFileName.Substring(iLen - 4, 4)
        If sExtension = ".xml" Then retval = True
        Return retval
    End Function

    Private Function ImportXMLAttachment(ByVal oAttachment As Outlook.Attachment, exs As List(Of Exception)) As Results
        'Try
        Dim TmpFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Dim TmpFile As String = TmpFolder & "\ImportMailAttachment.xml"
        oAttachment.SaveAsFile(TmpFile)
        Return ImportXMLFile(exs, TmpFile, oAttachment.DisplayName)
        'Catch ex As Exception
        'Log(Results.Err, "XMLATTACH", oAttachment.DisplayName & ": " & ex.Message)
        'Return Results.Err
        'End Try
    End Function

    Private Function ImportXMLFile(exs As List(Of Exception), ByVal sFileFullPath As String, Optional ByVal sFileDisplayName As String = "") As Results
        Dim retval As Results = Results._NotSet
        If sFileDisplayName = "" Then sFileDisplayName = sFileFullPath
        Dim oDoc As New Xml.XmlDocument
        oDoc.Load(sFileFullPath)
        Dim sTipo As String = oDoc.DocumentElement.GetAttribute("TIPO")
        If sTipo = "" Then sTipo = oDoc.DocumentElement.GetAttribute("TYPE")
        Select Case sTipo
            Case "SALIDASALMACEN"
                retval = Results.Omit
            Case ""
                Log(Results.Err, "XMLERROR", sFileDisplayName & ": TIPO no especificat")
                retval = Results.Err
            Case Else
                Log(Results.Err, "XMLERROR", sFileDisplayName & ": TIPO '" & sTipo & "' no reconegut")
                retval = Results.Err
        End Select
        'Catch ex As Exception
        'Log(Results.Err, "XML", sFileDisplayName & ": " & ex.Message)
        'maxisrvr.MailErr(ex.StackTrace)
        'Return Results.Err
        'End Try
        Return retval
    End Function

    Private Sub Log(ByVal oResult As Results, ByVal sSource As String, ByVal sTxt As String)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow()
        oRow(Cols.Result) = oResult
        oRow(Cols.Source) = sSource
        oRow(Cols.Txt) = sTxt
        oTb.Rows.Add(oRow)
        If oResult <> Results.Info Then mDirtyErrs = True
        Application.DoEvents()
    End Sub

    Private Sub CreateDatatable()
        Dim oTb As New DataTable("TELS")
        oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oTb.Columns.Add("RESULT", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("SOURCE", System.Type.GetType("System.String"))
        oTb.Columns.Add("TXT", System.Type.GetType("System.String"))
        mDs = New DataSet
        mDs.Tables.Add(oTb)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Result)
                .Visible = False
            End With
            With .Columns(Cols.Source)
                .HeaderText = "fitxer"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "observacions"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If IsDBNull(oRow.Cells(Cols.Result).Value) Then
                    e.Value = My.Resources.empty
                Else
                    Dim oCod As Results = DirectCast(oRow.Cells(Cols.Result).Value, Results)
                    Select Case oCod
                        Case Results.Info
                            e.Value = My.Resources.info
                        Case Results.Warn
                            e.Value = My.Resources.warn
                        Case Results.Err
                            e.Value = My.Resources.wrong
                        Case Else
                            e.Value = My.Resources.NoPark
                    End Select
                End If
        End Select
    End Sub

End Class
