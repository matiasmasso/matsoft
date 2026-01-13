

Public Class Frm_eDiversaInboxErrors

    Private _Client As Client
    Private mEdifile As EdiFile
    Private mErrs As List(Of Exception)
    Private mAllowEvents As Boolean
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        text
    End Enum

    Public Sub New(oEdiFile As EdiFile, Optional exs As List(Of Exception) = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mEdifile = oEdiFile
        mErrs = exs
        Dim oContact As Contact = Contact.FromGln(mEdifile.Sender)
        _Client = New Client(oContact.Guid)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(oEdiFile.SenderEanOrNom)
        sb.AppendLine(oEdiFile.Fch.ToShortDateString)
        sb.AppendLine(oEdiFile.Filename)
        TextBox1.Text = sb.ToString
        LoadGrid()
    End Sub


    Private Sub LoadGrid()

        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = mErrs
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.text)
                .DataPropertyName = "message"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With

        mAllowEvents = True
        SetContextMenu()
    End Sub

    Private Function CurrentItm() As Exception
        Dim idx As Integer = DataGridView1.CurrentCell.RowIndex
        Dim retval As Exception = mErrs(idx)
        Return retval
    End Function

    Private Function IsEci() As Boolean
        Dim oSenderEan As maxisrvr.ean13 = mEdifile.Sender
        Dim oSender As Client = New Client(Contact.FromGln(oSenderEan).Guid)
        Dim retval As Boolean = oSender.CcxOrMe.Equals(ElCorteIngles.Central)

        'Dim oEci As Contact = ElCorteIngles.Central
        'Dim oECI_EAN As maxisrvr.ean13 = oEci.Gln
        'If oECI_EAN IsNot Nothing Then
        'Dim sECI As String = oECI_EAN.ToString
        'retval = sECI = sSender
        'End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oMenuItem As ToolStripMenuItem
        Dim oContextMenuStrip As New ContextMenuStrip

        If DataGridView1.Rows.Count > 0 Then
            If DataGridView1.CurrentRow Is Nothing Then
                DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(Cols.text)
            End If
            Dim oRow As DataGridViewRow = DataGridView1.CurrentRow

            If oRow IsNot Nothing Then
                Dim ex As Exception = CurrentItm()
                If TypeOf ex Is EdifileException Then
                    Select Case CType(ex, EdifileException).ErrCod
                        Case EdifileException.Errs.ArtWrongPreu
                            oMenuItem = New MatMenuItem("modificar el preu a la comanda", ex)
                            AddHandler oMenuItem.Click, AddressOf Do_EditArtPreu
                            oContextMenuStrip.Items.Add(oMenuItem)
                            oMenuItem = New MatMenuItem("autoritzar el preu demanat per el client", ex)
                            AddHandler oMenuItem.Click, AddressOf Do_AuthorizeArtPreu
                            oContextMenuStrip.Items.Add(oMenuItem)
                        Case EdifileException.Errs.ArtEanMissing, EdifileException.Errs.ArtEanNotFound
                            Dim sCustRef As String = ""
                            Dim sRefPrv As String = ""
                            Dim oLineItem As EdiLineItem = CType(ex, EdifileException).Sender
                            If oLineItem.PIALIN_Client IsNot Nothing Then
                                sCustRef = oLineItem.PIALIN_Client.Referencia
                                Dim oArts As Arts = Arts.FromCustRef(_Client, sCustRef)
                                For Each oArt As Art In oArts
                                    oMenuItem = New ToolStripMenuItem("ref cli." & sCustRef & " -> " & oArt.Nom_ESP & " [" & oArt.Id & "]", Nothing, AddressOf Do_InsertSuggestedArt)
                                    oContextMenuStrip.Items.Add(oMenuItem)
                                Next
                            End If
                            If oLineItem.PIALIN_Proveidor IsNot Nothing Then
                                sRefPrv = oLineItem.PIALIN_Proveidor.Referencia
                                Dim oArts As Arts = Arts.FromRefPrv(sRefPrv)
                                For Each oArt As Art In oArts
                                    oMenuItem = New ToolStripMenuItem("ref prv." & sRefPrv & " -> " & oArt.Nom_ESP & " [" & oArt.Id & "]", Nothing, AddressOf Do_InsertSuggestedArt)
                                    oContextMenuStrip.Items.Add(oMenuItem)
                                Next
                            End If

                            Dim sb As New System.Text.StringBuilder
                            If sCustRef > "" Then sb.Append(" ref cli." & sCustRef)
                            If sRefPrv > "" Then sb.Append(" ref prv." & sRefPrv)
                            If oLineItem.Descripcio > "" Then sb.Append(" dsc." & oLineItem.Descripcio)

                            oMenuItem = New ToolStripMenuItem("afegir codi EAN" & sb.ToString, Nothing, AddressOf Do_InsertEAN)
                            oContextMenuStrip.Items.Add(oMenuItem)

                            oMenuItem = New ToolStripMenuItem("seleccionar un altre article" & sb.ToString, Nothing, AddressOf Do_SelectNewArt)
                            oContextMenuStrip.Items.Add(oMenuItem)


                        Case EdifileException.Errs.WrongPlatform
                            If IsEci() Then
                                Dim sLines() As String = System.Text.RegularExpressions.Regex.Split(mEdifile.ToString, Environment.NewLine)

                                Dim oPlatfMenuItem As New ToolStripMenuItem("plataforma")
                                oContextMenuStrip.Items.Add(oPlatfMenuItem)

                                Dim oNADDP_Ean As MaxiSrvr.Ean13 = CType(mEdifile, EdiFile_ORDERS_D_96A_UN_EAN008).NADDP.Ean
                                For Each plataforma As Contact In ElCorteIngles.Plataformas()
                                    oMenuItem = New ToolStripMenuItem(New Client(plataforma.Guid).Referencia, Nothing, AddressOf Do_EciSwitchPlatform)
                                    oMenuItem.CheckOnClick = True
                                    If plataforma.Gln.Equals(oNADDP_Ean) Then
                                        oMenuItem.Checked = True
                                    End If
                                    oPlatfMenuItem.DropDownItems.Add(oMenuItem)
                                Next

                            End If

                        Case EdifileException.Errs.EmisorNotFound
                            oMenuItem = New ToolStripMenuItem("Buscar notificacions sobre aquest emissor", Nothing, AddressOf Do_SearchEmisor)
                            oContextMenuStrip.Items.Add(oMenuItem)
                            oMenuItem = New ToolStripMenuItem("Copiar EAN del emissor", Nothing, AddressOf Do_CopyEmisor)
                            oContextMenuStrip.Items.Add(oMenuItem)

                    End Select
                Else
                End If
            End If
        End If

        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub Do_EditArtPreu(sender As Object, e As EventArgs)
        Dim oMenuItem As MatMenuItem = sender
        Dim oErr As ValidationError = oMenuItem.CustomObject
        Dim oEdiLineItem As EdiLineItem = oErr.Sender
        Dim oEdiFile As EdiFile_ORDERS_D_96A_UN_EAN008 = oEdiLineItem.Parent
        Dim oContact As Contact = Contact.FromGln(oEdiFile.NADBY.Ean)
        Dim oClient As New Client(oContact.Guid)
        Dim oArtEAN As Ean13 = oEdiLineItem.ArtEan
        Dim oArt As Art = ArtLoader.FromEan(oArtEAN)
        Dim oPreuTarifa As DTOAmt = oArt.Tarifa(oClient)


        Dim oSegments As List(Of EdiSegment) = oEdiLineItem.Segments.FindAll(Function(x) x.Tag = EdiSegment.Tags.PRILIN)
        For Each oSegment As EdiSegment_PRILIN In oSegments
            If oSegment.Tipo = EdiSegment_PRILIN.Tipos.AAB Then
                'Stop
                oSegment.SetValue(1, oPreuTarifa)
                Dim exs as New List(Of exception)
                If oEdiFile.Update( exs) Then
                    Reprocesa(mEdifile)
                Else
                    UIHelper.WarnError( exs, "error al canviar el producte")
                End If
                Exit Sub
            End If
        Next

        Stop
    End Sub

    Private Sub Do_AuthorizeArtPreu(sender As Object, e As EventArgs)
        Dim oMenuItem As MatMenuItem = sender
        Dim oErr As ValidationError = oMenuItem.CustomObject
        Dim oEdiLineItem As EdiLineItem = oErr.Sender
        'oEdiLineItem()
        MsgBox("no implementat encara, sorry", MsgBoxStyle.Exclamation)
    End Sub

    Private Sub Do_SearchEmisor(sender As Object, e As System.EventArgs)
        Dim oErr As Exception = CurrentItm()
        Dim sErrText As String = oErr.Message
        Dim regex As New System.Text.RegularExpressions.Regex("[0-9]{13}$")
        Dim match As System.Text.RegularExpressions.Match = regex.Match(sErrText)
        If match.Success Then
            Dim sEAN13 As String = match.Value
            Dim oFiles As EdiFiles = EdiFiles.All(EdiFile.IOcods.Inbox, EdiFile.Tags.GENRAL_D_96A_UN_EAN003.ToString, EdiFiles.Filters.ShowAll, sEAN13)
            Select Case oFiles.Count
                Case 0
                    MsgBox("no hi ha cap missatge sobre aquest emisor", MsgBoxStyle.Exclamation, "MAT.NET")
                Case 1
                    root.ShowLiteral("Emissor " & sEAN13, oFiles(0).Text)
                Case Else
                    MsgBox("hi han " & oFiles.Count.ToString & " missatges sobre l'emisor " & sEAN13 & "." & vbCrLf & "A continuació mostrarem el darrer missatge", MsgBoxStyle.Information, "MAT.NET")
                    root.ShowLiteral("Emissor " & sEAN13, oFiles(0).Text)
            End Select
        End If
    End Sub

    Private Sub Do_CopyEmisor(sender As Object, e As System.EventArgs)
        Dim oErr As Exception = CurrentItm()
        Dim sErrText As String = oErr.Message
        Dim regex As New System.Text.RegularExpressions.Regex("[0-9]{13}$")
        Dim match As System.Text.RegularExpressions.Match = regex.Match(sErrText)
        If match.Success Then
            Dim sEAN13 As String = match.Value
            Clipboard.SetDataObject(sEAN13, True)
            MsgBox(sEAN13 & " copiat a portapapers", MsgBoxStyle.Information, "MAT.NET")
        Else
            MsgBox("No s'ha trobat cap codi EAN a la rescripció de l'error", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub



    Private Sub Do_InsertEAN(sender As Object, e As System.EventArgs)
        Dim oProduct As Product = sender
        Dim oArt As New Art(oProduct.Guid)
        Dim s As String = InputBox("codi EAN de " & oArt.Nom_ESP)
        If s > "" Then
            Dim oEan As New MaxiSrvr.Ean13(s)
            If oEan.IsValid Then
                oArt.SetItm()
                oArt.Cbar = oEan

                Dim exs as New List(Of exception)
                If oArt.Update( exs) Then
                    MsgBox("article actualitzat. Cal fer correr altra vegada la bandeja de Edi", MsgBoxStyle.Information)
                Else
                    MsgBox("error al desar l'article" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            Else
                MsgBox("EAN no valid", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub Do_SelectNewArt(sender As Object, e As System.EventArgs)
        Dim oFrm As New Frm_Products(, Frm_Products.SelModes.SelectProductSku)
        AddHandler oFrm.onItemSelected, AddressOf onMissingArtSelected
        oFrm.Show()
    End Sub

    Private Sub Do_EciSwitchPlatform(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oNewPlatform As maxisrvr.ean13 = Nothing
        Dim sPlatformNom As String = ""
        For Each plataforma As Contact In ElCorteIngles.Plataformas()
            Dim sItemNom As String = New Client(plataforma.Guid).Referencia
            If oMenuItem.Text = sItemNom Then
                oNewPlatform = plataforma.Gln
                sPlatformNom = sItemNom
                Exit For
            End If
        Next

        If oNewPlatform Is Nothing Then
            MsgBox("operacio no realitzada, plataforma no valida", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            If mEdifile.ChangeFieldValueFromFirstTagNamed(EdiSegment.Tags.NADDP, 0, oNewPlatform.Value) Then
                Dim exs as New List(Of exception)
                If mEdifile.Update( exs) Then
                    Dim oEdiOrderFile As New EdiFile_ORDERS_D_96A_UN_EAN008(mEdifile.Guid)
                    ElCorteIngles.warnNewPlatform(oEdiOrderFile.DocumentNumber, sPlatformNom)
                    MsgBox("comanda actualitzada a la plataforma de " & sPlatformNom, MsgBoxStyle.Information)
                Else
                    UIHelper.WarnError("error al canviar la plataforma")
                End If
            End If
        End If
    End Sub

    Private Sub Do_InsertSuggestedArt(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim sMenuText As String = oMenuItem.Text
        Dim sArtId As String = BLL.TextHelper.SelectBetween(sMenuText, "[", "]")
        Dim oArt As Art = Art.FromNum(BLL.BLLApp.Emp, CInt(sArtId))

        If oArt IsNot Nothing Then
            Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
            Dim oErr As ValidationError = oRow.DataBoundItem
            Dim oLine As EdiLineItem = oErr.Sender

            Dim oSegment_Lins As List(Of EdiSegment) = mEdifile.Segments.FindAll(Function(x) x.Tag = EdiSegment.Tags.LIN)
            Dim oSegment As EdiSegment_LIN = oSegment_Lins.Find(Function(x) x.Values(2) = oLine.LineNumber)

            oSegment.ArtEan = oArt.Cbar
            Dim exs as New List(Of exception)
            If mEdifile.Update( exs) Then
                Reprocesa(mEdifile)
            Else
                UIHelper.WarnError( exs, "error al canviar el producte")
            End If
        Else
            MsgBox("article amb codi Ean no valid", MsgBoxStyle.Exclamation, oArt.Nom_ESP)
        End If

    End Sub

    Private Sub onMissingArtSelected(sender As Object, e As MatEventArgs)
        Dim oSku As DTOProductSku = e.Argument
        BLLProductSku.Load(oSku)

        If oSku.Ean13.Value.Length = 13 Then
            Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
            Dim oErr As EdifileException = oRow.DataBoundItem
            Dim oLine As EdiLineItem = oErr.Sender
            Dim oEdiFile As EdiFile = oLine.Parent

            Dim oSegment As EdiSegment_LIN = oEdiFile.Segments.Find(Function(x) x.Tag = EdiSegment.Tags.LIN)
            oSegment.ArtEan = New Ean13(oSku.Ean13.Value)
            Dim exs As New List(Of Exception)
            If oEdiFile.Update(exs) Then
                Reprocesa(oEdiFile)
            Else
                UIHelper.WarnError(exs, "error al canviar el producte")
            End If
        Else
            MsgBox("article amb codi Ean no valid", MsgBoxStyle.Exclamation, oSku.NomLlarg)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Reprocesa(oEdiFile As EdiFile_ORDERS_D_96A_UN_EAN008)
        Dim exs as new list(Of Exception)
        mEdifile.ValidationErrors = exs
        If mEdifile.Procesa( exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oEdiFile))
            Me.Close()
        Else

            LoadGrid()
        End If

    End Sub
End Class