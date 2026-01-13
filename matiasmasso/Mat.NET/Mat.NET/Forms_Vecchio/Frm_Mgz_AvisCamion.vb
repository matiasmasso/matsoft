Imports System.Xml

Public Class Frm_Mgz_AvisCamion
    Private mDs As DataSet
    Private mProveidor As Proveidor
    Private mEmp As DTOEmp

    Private mLastValidatedObject As Object
    Private mDirtyCell As Boolean

    Private Enum Cols
        Id
        Nom
        Qty
    End Enum

    Public WriteOnly Property Proveidor() As Proveidor
        Set(ByVal value As Proveidor)
            mProveidor = value
            mEmp = BLL.BLLApp.Emp
            Me.Text = "AVIS A VIVACE DE ARRIBADA DE " & mProveidor.Nom_o_NomComercial
            PictureBoxLogo.Image = mProveidor.Img48
            LoadGrid()
        End Set
    End Property

    Private Function CreateDataTable() As DataTable
        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("ART", System.Type.GetType("System.Int32")))
            .Add(New DataColumn("NOM", System.Type.GetType("System.String")))
            .Add(New DataColumn("QTY", System.Type.GetType("System.Int32")))
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

            With .Columns(Cols.Id)
                .HeaderText = "ref"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
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
        End With
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim sFilename As String = SaveDoc()
        SendMail(sFilename)
        Try

            My.Computer.FileSystem.DeleteFile(sFilename)
        Catch ex As Exception

        End Try
        Me.Close()
    End Sub

    Private Function SaveDoc() As String

        Dim oDom As New Xml.XmlDocument

        Dim sNum As String = Guid.NewGuid().ToString.Substring(1, 4)

        Dim oNodeRoot As XmlElement = oDom.CreateElement("DOCUMENT")
        oNodeRoot.SetAttribute("TYPE", "AVISOCAMION")
        oNodeRoot.SetAttribute("DATE", Format(DateTimePicker1.Value, "dd/MM/yyyy"))
        oNodeRoot.SetAttribute("NUMERO", sNum)
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
        oNodeExp.SetAttribute("NUMPROVEEDOR", mProveidor.Id.ToString)
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
            oNodeItem.SetAttribute("QTY", oRow(Cols.Qty).ToString)
            oNodeItem.SetAttribute("REF", oRow(Cols.Id).ToString)
            oNodeItem.SetAttribute("DSC", oRow(Cols.Nom).ToString)
            oNodeItems.AppendChild(oNodeItem)
        Next

        Dim sFilename As String = maxisrvr.TmpFolder & "AvisArribadaCamion " & sNum & ".xml"
        oDom.Save(sFilename)
        Return sFilename
    End Function

    Public Sub SendMail(ByVal sFilename As String)
        Dim oMgz As Mgz = New Mgz(BLL.BLLApp.Mgz.Guid)
        Dim sTo As String = oMgz.Email
        Dim oSsc As New DTOSubscription(DTOSubscription.Ids.AvisArribadaCamion)
        Dim oAttachments As New ArrayList
        oAttachments.Add(sFilename)
       BLL.MailHelper.SendMail(sTo, BLL.BLLSubscription.EmailAddressCollection(oSsc), , Me.Text, , , oAttachments)
    End Sub

    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Id).Value) Then
                Dim LngId As Long = oRow.Cells(Cols.Id).Value
                oArt = MaxiSrvr.Art.FromNum(mEmp, LngId)
            End If
        End If
        Return oArt
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oArt As Art = Nothing

        If oArt IsNot Nothing Then
            Dim oMenu_Art As New Menu_Art(oArt)
            'AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Art.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        mDirtyCell = True
    End Sub

    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            Select Case e.ColumnIndex
                Case Cols.Id
                    If IsNumeric(e.FormattedValue) Then
                        Dim oArt As Art = MaxiSrvr.Art.FromNum(BLL.BLLApp.Emp, e.FormattedValue)
                        If oArt.Exists Then
                            mLastValidatedObject = oArt
                        Else
                            e.Cancel = True
                        End If
                    Else
                        mLastValidatedObject = Nothing
                    End If
                Case Cols.Nom
                    If e.FormattedValue = "" Then
                        mLastValidatedObject = Nothing
                    Else
                        Dim oSku As ProductSku = Finder.FindSku(BLL.BLLApp.Mgz, e.FormattedValue)
                        If oSku Is Nothing Then
                            e.Cancel = True
                        Else
                            mLastValidatedObject = New Art(oSku.Guid)
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
                        oRow.Cells(Cols.Id).Value = 0
                        oRow.Cells(Cols.Nom).Value = ""
                    Else
                        Dim oArt As Art = CType(mLastValidatedObject, Art)
                        oRow.Cells(Cols.Id).Value = oArt.Id
                        oRow.Cells(Cols.Nom).Value = oArt.Nom_ESP
                    End If
            End Select
            mDirtyCell = False
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oArt As Art = CurrentArt()
        If oArt IsNot Nothing Then
            root.ShowArt(oArt)
        End If
    End Sub

    Private Sub DataGridView1_RowValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowValidated
        ButtonOk.Enabled = True
    End Sub
End Class