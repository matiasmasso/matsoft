Imports System.Xml

Public Class Frm_Mgz_AvisCamion
    Private mDs As DataSet
    Private mProveidor As DTOProveidor

    Private mLastValidatedObject As Object
    Private mDirtyCell As Boolean

    Private Enum Cols
        Guid
        Id
        Nom
        Qty
        Brand
    End Enum

    Public WriteOnly Property Proveidor() As DTOProveidor
        Set(ByVal value As DTOProveidor)
            mProveidor = value
        End Set
    End Property

    Private Sub Frm_Mgz_AvisCamion_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Contact.Load(mProveidor, exs) Then
            Me.Text = String.Format("Avís a Vivace de arribada de {0}", mProveidor.NomComercialOrDefault())
            PictureBoxLogo.Image = LegacyHelper.ImageHelper.Converter(mProveidor.Logo)
            LoadGrid()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function CreateDataTable() As DataTable
        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("GUID", System.Type.GetType("System.Guid")))
            .Add(New DataColumn("ART", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("NOM", System.Type.GetType("System.String")))
            .Add(New DataColumn("QTY", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("BRAND", System.Type.GetType("System.String")))
        End With
        Return oTb
    End Function

    Private Sub LoadGrid()
        mDs = New DataSet
        mDs.Tables.Add(CreateDataTable())
        Dim oTb As DataTable = mDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "ref"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .ReadOnly = True
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "Producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "quantitat"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Brand)
                .Visible = False
            End With
        End With
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim sFilename As String = SaveDoc()

        UIHelper.ToggleProggressBar(Panel1, True)
        Dim sSendTo As String = Await FEB.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.EmailTransmisioVivace, exs)
        UIHelper.ToggleProggressBar(Panel1, False)
        If exs.Count = 0 Then
            Dim oMailMessage = DTOMailMessage.Factory(sSendTo)
            With oMailMessage
                .cc = Await FEB.Subscriptors.Recipients(exs, GlobalVariables.Emp, DTOSubscription.Wellknowns.AvisArribadaCamion)
                .body = Me.Text
                .AddAttachment(sFilename)
            End With

            UIHelper.ToggleProggressBar(Panel1, True)
            If Await OutlookHelper.Send(oMailMessage, exs) Then
                My.Computer.FileSystem.DeleteFile(sFilename)
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Function SaveDoc() As String

        Dim oDom As New Xml.XmlDocument

        Dim sNum As String = Guid.NewGuid().ToString.Substring(1, 4)

        Dim oNodeRoot As XmlElement = oDom.CreateElement("DOCUMENT")
        oNodeRoot.SetAttribute("TYPE", "AVISOCAMION")
        oNodeRoot.SetAttribute("DATE", Format(DateTimePicker1.Value, "dd/MM/yyyy"))
        oNodeRoot.SetAttribute("NUMERO", sNum)
        oNodeRoot.SetAttribute("REMESA", Guid.Empty.ToString())
        'oNodeRoot.SetAttribute("TITLE", TextBoxObs.Text)
        oDom.AppendChild(oNodeRoot)

        Dim oNodeRte As XmlElement = oDom.CreateElement("REMITE")
        oNodeRte.SetAttribute("NIF", "A58007857")
        oNodeRte.SetAttribute("NOM", "MATIAS MASSO, S.A.")
        oNodeRoot.AppendChild(oNodeRte)

        Dim oNodeDestino As XmlElement = oDom.CreateElement("DESTINO")
        oNodeDestino.SetAttribute("NIF", "A62572342")
        oNodeDestino.SetAttribute("NOM", "VIVACE LOGISTICA, S.A.")
        oNodeRoot.AppendChild(oNodeDestino)

        Dim oNodeExp As XmlElement = oDom.CreateElement("EXPEDICION")
        oNodeExp.SetAttribute("NUMPROVEEDOR", mProveidor.Id.ToString())
        oNodeExp.SetAttribute("PROCEDENCIA", mProveidor.Nom)
        oNodeExp.SetAttribute("TRANSITARIO", TextBoxObs.Text)
        oNodeExp.SetAttribute("LLEGADA", DateTimePicker1.Value.ToShortDateString)
        oNodeExp.SetAttribute("BULTOS", "")
        oNodeExp.SetAttribute("KG", "")
        oNodeExp.SetAttribute("M3", "")
        oNodeRoot.AppendChild(oNodeExp)

        Dim oNodeItems As XmlElement = oDom.CreateElement("ITEMS")
        oNodeRoot.AppendChild(oNodeItems)

        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim oNodeItem As XmlElement
        For Each oRow In oTb.Rows
            oNodeItem = oDom.CreateElement("ITEM")
            oNodeItem.SetAttribute("QTY", oRow(Cols.Qty).ToString())
            oNodeItem.SetAttribute("REF", oRow(Cols.Id).ToString())
            oNodeItem.SetAttribute("DSC", oRow(Cols.Nom).ToString())
            oNodeItem.SetAttribute("BRAND", oRow(Cols.Brand).ToString())
            oNodeItem.SetAttribute("LIN", Guid.Empty.ToString())
            oNodeItems.AppendChild(oNodeItem)
        Next

        Dim sFilename As String = FileSystemHelper.TmpFolder & "\AvisArribadaCamion " & sNum & ".xml"
        oDom.Save(sFilename)
        Return sFilename
    End Function


    Private Function CurrentSku() As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Guid).Value) Then
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                retval = New DTOProductSku(oGuid)
            End If
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oSku As DTOProductSku = CurrentSku()

        If oSku IsNot Nothing Then
            Dim oMenu_Sku As New Menu_ProductSku(oSku)
            'AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Sku.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        mDirtyCell = True
    End Sub

    Private Async Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        Dim exs As New List(Of Exception)
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            Select Case e.ColumnIndex
                Case Cols.Id
                    If IsNumeric(e.FormattedValue) Then
                        Dim oSku = Await FEB.ProductSku.FromId(exs, Current.Session.Emp, e.FormattedValue)
                        If exs.Count = 0 Then
                            If oSku Is Nothing Then
                                e.Cancel = True
                            Else
                                mLastValidatedObject = oSku
                            End If
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        mLastValidatedObject = Nothing
                    End If
                Case Cols.Nom
                    If e.FormattedValue = "" Then
                        mLastValidatedObject = Nothing
                    Else
                        Dim oSku = Await Finder.FindSku(exs, Current.Session.Emp, e.FormattedValue, GlobalVariables.Emp.Mgz)
                        If exs.Count = 0 Then
                            If oSku Is Nothing Then
                                e.Cancel = True
                            Else
                                mLastValidatedObject = oSku
                            End If
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Select Case e.ColumnIndex
                Case Cols.Id, Cols.Nom
                    If mLastValidatedObject Is Nothing Then
                        oRow.Cells(Cols.Guid).Value = System.DBNull.Value
                        oRow.Cells(Cols.Id).Value = 0
                        oRow.Cells(Cols.Nom).Value = ""
                        oRow.Cells(Cols.Brand).Value = ""
                    Else
                        Dim oSku = DirectCast(mLastValidatedObject, DTOProductSku)
                        oRow.Cells(Cols.Guid).Value = oSku.Guid
                        oRow.Cells(Cols.Id).Value = oSku.Id
                        oRow.Cells(Cols.Nom).Value = oSku.NomLlarg.Tradueix(Current.Session.Lang)
                        oRow.Cells(Cols.Brand).Value = oSku.brandNom()
                    End If
            End Select
            mDirtyCell = False
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oSku As DTOProductSku = CurrentSku()

        If oSku IsNot Nothing Then
            Dim oFrm As New Frm_Art(oSku)
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        ButtonOk.Enabled = True
    End Sub


End Class