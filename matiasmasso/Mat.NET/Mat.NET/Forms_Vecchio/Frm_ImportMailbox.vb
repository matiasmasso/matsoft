
Imports Microsoft.Office.Interop

Public Class Frm_ImportMailbox

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
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

    Private mSkipMailBox As Boolean

    Public WriteOnly Property FileName() As String
        Set(ByVal Value As String)
            mSkipMailBox = True
            CreateDatatable()
            Me.Show()
            ImportXMLFile(Value)
        End Set
    End Property


    Private Sub Frm_ImportMailbox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor = Cursors.WaitCursor
        'Try
        If Not mSkipMailBox Then
            CreateDatatable()
            Me.Show()
            'Application.DoEvents()
            ImportMailBoxAttachments()
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

    Public Sub ImportMailBoxAttachments()

        Dim oApp As Outlook.Application = MatOutlook.OutlookApp
        Dim oInbox As Outlook.MAPIFolder = MatOutlook.OutlookNameSpace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox)
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
                oMsg = CType(oObj, Outlook.MailItem)
                Dim s As String = oMsg.ReceivedTime.ToShortDateString & " " & oMsg.Subject
                ImportMailBoxAttachment(oMsg)
            End If
            'Catch ex2 As InvalidCastException
            'MsgBox("halt! invalid cast exception")
            'Catch ex As Exception
            'Log(Results.Err, "MSG", oMsg.SenderName & ": " & oMsg.Subject)
            'Finally
            ProgressBar1.Increment(1)
            'End Try
        Next oObj

    End Sub

    Public Sub ImportMailBoxAttachment(ByVal oMsg As Outlook.MailItem)
        Dim oAttach As Outlook.Attachment
        Dim oResult As Results

        'Try

        'If oMsg.SenderName = "Eva Macayo" Then Stop
        Select Case oMsg.SenderName
            Case "Universal Currency Converter"
                If UpdateCurrencyRates(oMsg.Body) = Results.Info Then
                    oMsg.Delete()
                End If

            Case Else
                oResult = Results._NotSet
                For Each oAttach In oMsg.Attachments
                    If IsXML(oAttach.FileName) Then
                        Select Case ImportXMLAttachment(oAttach)
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

    End Sub

    Private Function IsXML(ByVal sFileName As String) As Boolean
        Dim retval As Boolean
        Dim iLen As Integer = sFileName.Length
        Dim sExtension As String = sFileName.Substring(iLen - 4, 4)
        If sExtension = ".xml" Then retval = True
        Return retval
    End Function

    Private Function ImportXMLAttachment(ByVal oAttachment As Outlook.Attachment) As Results
        'Try
        Dim TmpFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Dim TmpFile As String = TmpFolder & "\ImportMailAttachment.xml"
        oAttachment.SaveAsFile(TmpFile)
        Return ImportXMLFile(TmpFile, oAttachment.DisplayName)
        'Catch ex As Exception
        'Log(Results.Err, "XMLATTACH", oAttachment.DisplayName & ": " & ex.Message)
        'Return Results.Err
        'End Try
    End Function

    Private Function ImportXMLFile(ByVal sFileFullPath As String, Optional ByVal sFileDisplayName As String = "") As Results
        Dim retval As Results = Results._NotSet
        If sFileDisplayName = "" Then sFileDisplayName = sFileFullPath
        Dim oDoc As New Xml.XmlDocument
        oDoc.Load(sFileFullPath)
        Dim sTipo As String = oDoc.DocumentElement.GetAttribute("TIPO")
        If sTipo = "" Then sTipo = oDoc.DocumentElement.GetAttribute("TYPE")
        Select Case sTipo
            Case "TRANSMTRP"
                Dim oAlbConfirm As New AlbConfirm(mEmp, oDoc)
                Dim sErr As String = oalbConfirm.ImportErrors
                If sErr = "" Then
                    oAlbConfirm.Update(sErr)
                    Log(Results.Info, sTipo, "remesa " & oAlbConfirm.Id & " del " & oAlbConfirm.Fch)
                    retval = Results.Info
                Else
                    Log(Results.Err, sTipo, sFileDisplayName & ": " & sErr)
                    retval = Results.Err
                End If
            Case "PEDIDOCLIENTE"
                Try
                    Dim oPdc As New Pdc(BLL.BLLApp.Emp, DTOPurchaseOrder.Codis.client)
                    Dim sXML As String = My.Computer.FileSystem.ReadAllText(sFileFullPath)
                    Dim sResult As String = oPdc.LoadXML(sXML)
                    If sResult = "" Then
                        Dim exs as New List(Of exception)
                        If oPdc.Update( exs) Then
                            If oPdc.InStk Then
                                Dim oAlb As Alb = oPdc.Deliver()
                                oAlb.SetUser(BLL.BLLSession.Current.User)
                                If oAlb.Update(exs) Then
                                    sResult = "Pedido registrado y enviado en albarán " & oAlb.Id
                                Else
                                    sResult = "Error al registrar l'albará" & vbCrLf & BLL.Defaults.ExsToMultiline(exs)
                                End If
                                'Dim oDoc As New System.Xml.XmlDocument
                                'oDoc.LoadXml(XMLString)
                                Dim oNodeURL As System.Xml.XmlElement = oDoc.DocumentElement.SelectSingleNode("DOCUMENTACION")
                                Dim sURL As String = oNodeURL.GetAttribute("URL")
                                oAlb.MailLogistica(sURL)
                            End If
                        Else
                            sResult = "error al registrar comanda" & vbCrLf & BLL.Defaults.ExsToMultiline(exs)
                        End If
                    End If
                    MsgBox(sResult)
                    'MailHelper.SendMail("info@matiasmasso.es", "COMANDA XML " & oPdc.Id & " DE " & oPdc.Client.Clx)
                    'Log(Results.Info, sTipo, "comanda " & oPdc.Id & " de " & oPdc.Client.Nom_o_NomComercial)
                    'Return Results.Info
                Catch ex As Exception
                    BLL.MailHelper.SendMail("info@matiasmasso.es", , , "ERROR EN COMANDA XML!")
                    Log(Results.Err, "XMLERROR", sFileDisplayName & ": Error en comanda XML")
                    retval = Results.Err
                End Try
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
                    Dim oCod As Results = CType(oRow.Cells(Cols.Result).Value, Results)
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

    Private Function UpdateCurrencyRates(ByVal StBody As String) As Results
        Dim retval As Results = Results._NotSet
        Dim DtFch As Date
        Try
            Dim IntTmp As Integer = StBody.IndexOf("Rates as of")
            If IntTmp < 0 Then
                Return retval
                Exit Function
            End If

            Dim StFch As String = StBody.Substring(IntTmp + 12, 10)
            DtFch = CDate(StFch.Substring(8, 2) & "/" & StFch.Substring(5, 2) & "/" & StFch.Substring(0, 4))
            Dim SQLfch As String = Format(DtFch, "yyyyMMdd")

            Dim StTmp As String
            Dim StLine As String
            Dim ISO As String
            Dim StEurs As String
            Dim StExchange As String
            Dim SQL As String

            Dim LineFeed As New String(Chr(10), 1)

            IntTmp = StBody.IndexOf("USD United States Dollars")
            StTmp = StBody.Substring(IntTmp)
            IntTmp = StTmp.IndexOf(LineFeed)
            StLine = StTmp.Substring(0, IntTmp - 1)
            StTmp = StTmp.Substring(IntTmp + 1)
            Do While StLine > " "
                ISO = StLine.Substring(0, 3)
                StEurs = StLine.Substring(36, 19).Replace(",", "")
                StExchange = StLine.Substring(59).Replace(",", "") 'TREU LA COMA DELS MILERS
                SQL = "UPDATE CUR SET EUROS=" & StEurs & ", ONEEURO=" & StExchange & ", FCH='" & SQLfch & "' WHERE ID LIKE '" & ISO & "' AND FCH<='" & SQLfch & "'"
                MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi)
                IntTmp = StTmp.IndexOf(LineFeed)
                StLine = StTmp.Substring(0, IntTmp - 1)
                StTmp = StTmp.Substring(IntTmp + 1)
            Loop

            retval = Results.Info

        Catch ex As Exception
            Log(Results.Err, "DIVISAS", ex.Message)
            retval = Results.Err
        End Try

        Dim oCur As Cur = Current.Cur("GBP")
        Log(Results.Info, "DIVISAS", DtFch.ToShortDateString & " GBP a " & oCur.Euros & " EUR")

        Return retval
    End Function
End Class
