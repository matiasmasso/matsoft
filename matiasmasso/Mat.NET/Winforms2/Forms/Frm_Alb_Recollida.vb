

Public Class Frm_Alb_Recollida
    Private mRecollida As DTORecollida

    Private Enum Cols
        ArtGuid
        Qty
        Nom
    End Enum

    Public Sub New(oRecollida As DTORecollida)
        MyBase.New()
        Me.InitializeComponent()
        mRecollida = oRecollida
        refresca()
    End Sub

    Private Sub refresca()
        LoadEstats()
        LoadCarrecs()
        LoadAccions()

        With mRecollida
            Me.Text = "recollida albará " & .Delivery.Id
            DateTimePicker1.Value = .Fch

            TextBoxOrigenNom.Text = .OrigenNom
            TextBoxOrigenAdr.Text = .OrigenAdr
            Xl_CitOrigen.Load(.OrigenZip)
            TextBoxOrigenTel.Text = .OrigenTel
            TextBoxOrigenContact.Text = .OrigenContact
            TextBoxBultos.Text = .Bultos
            TextBoxMotiu.Text = .Motiu

            TextBoxDestiNom.Text = .DestiNom
            TextBoxDestiAdr.Text = .DestiAdr
            Xl_CitDesti.Load(.DestiZip)
            TextBoxDestiTel.Text = .DestiTel
            TextBoxDestiContact.Text = .DestiContact
        End With
        LoadGrid()
        ButtonOk.Enabled = True
    End Sub

    Private Sub LoadGrid()
        Dim oTb As New DataTable
        oTb.Columns.Add(New DataColumn("ArtGuid", System.Type.GetType("System.Guid")))
        oTb.Columns.Add(New DataColumn("Qty", System.Type.GetType("System.Int32")))
        oTb.Columns.Add(New DataColumn("Nom", System.Type.GetType("System.String")))

        For Each oItem As DTOQtySku In mRecollida.Items
            Dim oRow As DataRow = oTb.NewRow
            oRow(Cols.ArtGuid) = oItem.Sku.Guid
            oRow(Cols.Qty) = oItem.Qty
            oRow(Cols.Nom) = oItem.Sku.nomLlarg.Tradueix(Current.Session.Lang)
            oTb.Rows.Add(oRow)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .AllowUserToDeleteRows = True
            .DataSource = oTb
            '.SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .AllowUserToResizeRows = False
            .ReadOnly = False


            With .Columns(Cols.ArtGuid)
                .Visible = False
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "Quantitat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                '.ReadOnly = True
            End With
        End With
    End Sub

    Private Function GetItemsFromGrid() As List(Of DTOQtySku)
        Dim oItems As New List(Of DTOQtySku)
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            Dim iQty As Integer = oRow.Cells(Cols.Qty).Value
            Dim oGuid As Guid = oRow.Cells(Cols.ArtGuid).Value
            Dim oSku As New DTOProductSku(oGuid)
            Dim oItem As New DTOQtySku(iQty, oSku)
            oItems.Add(oItem)
        Next
        Return oItems
    End Function

    Private Sub LoadEstats()
        UIHelper.LoadComboFromEnum(ComboBoxEstat, GetType(DTORecollida.EstatsDeLaMercancia))
    End Sub

    Private Sub LoadCarrecs()
        UIHelper.LoadComboFromEnum(ComboBoxCarrec, GetType(DTORecollida.Carrecs))
    End Sub

    Private Sub LoadAccions()
        UIHelper.LoadComboFromEnum(ComboBoxAccio, GetType(DTORecollida.Accions))
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        With mRecollida
            .Fch = DateTimePicker1.Value

            .OrigenNom = TextBoxOrigenNom.Text
            .OrigenAdr = TextBoxOrigenAdr.Text
            .OrigenZip = Xl_CitOrigen.Value
            .OrigenTel = TextBoxOrigenTel.Text
            .OrigenContact = TextBoxOrigenContact.Text
            .Bultos = TextBoxBultos.Text
            .EstatDeLaMercancia = ComboBoxEstat.SelectedIndex
            .Motiu = TextBoxMotiu.Text

            .DestiNom = TextBoxDestiNom.Text
            .DestiAdr = TextBoxDestiAdr.Text
            .DestiZip = Xl_CitDesti.Value
            .DestiTel = TextBoxDestiTel.Text
            .DestiContact = TextBoxDestiContact.Text
            .Carrec = ComboBoxCarrec.SelectedIndex
            .Accio = ComboBoxAccio.SelectedIndex

            .Items = GetItemsFromGrid()

        End With
        MailMessage()
        Me.Close()
    End Sub

    Private Function GetHtmlTableFromItems(oitems As List(Of DTOQtySku)) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("<table>")
        sb.Append("<tr><td align='right'>quantitat</td><td align='left'>article</td></tr>")
        For Each oItem As DTOQtySku In oitems
            sb.Append("<tr>")
            sb.Append("<td>")
            sb.Append(oItem.Qty.ToString())
            sb.Append("</td>")
            sb.Append("<td>")
            sb.Append(oItem.Sku.nomLlarg.Tradueix(Current.Session.Lang))
            sb.Append("</td>")
            sb.Append("</tr>")
        Next
        sb.Append("</table>")
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Private Async Sub MailMessage()
        Dim exs As New List(Of Exception)

        Dim oMgz As DTOMgz = mRecollida.Delivery.Mgz
        If FEB.Contact.Load(oMgz, exs) Then
            Dim oLang As DTOLang = oMgz.Lang
            Dim sTo As String = Await FEB.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.EmailTransmisioVivace, exs)
            If exs.Count = 0 Then
                Dim oTxt = Await FEB.Txt.Find(DTOTxt.Ids.RecollidaMagatzem, exs)
                If exs.Count = 0 Then
                    Dim sBody As String = oTxt.ToHtml(
                mRecollida.Delivery.Mgz.Lang,
                mRecollida.OrigenNom,
                mRecollida.OrigenAdr,
                DTOZip.FullNom(mRecollida.OrigenZip),
                mRecollida.Bultos,
                mRecollida.EstatDeLaMercancia.ToString,
                mRecollida.Motiu,
                mRecollida.DestiNom,
                mRecollida.DestiAdr,
                DTOZip.FullNom(mRecollida.DestiZip),
                mRecollida.DestiTel,
                mRecollida.DestiContact,
                mRecollida.Carrec.ToString,
                mRecollida.Accio.ToString,
                GetHtmlTableFromItems(mRecollida.Items))

                    If exs.Count = 0 Then
                        Dim oMailMessage = DTOMailMessage.Factory(sTo)
                        With oMailMessage
                            .Subject = String.Format("{0} {1}", oLang.Tradueix("Recogida", "Recullida", "Callback"), mRecollida.Id)
                            .Body = sBody
                        End With

                        If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class