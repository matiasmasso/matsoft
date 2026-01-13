

Public Class Frm_Banc_Impago
    Private _Banc As DTOBanc
    Private _Impagats As List(Of DTOImpagat)
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Ref
        Eur
        Vto
        Txt
        Nom
    End Enum

    Public Sub New(oImpagats As List(Of DTOImpagat))
        MyBase.New
        Me.InitializeComponent()
        _Impagats = oImpagats
    End Sub

    Private Sub Frm_Banc_Impago_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Impagats.Count > 0 Then
            _Banc = _Impagats.First.Csb.Csa.banc
            Me.Text = String.Format("{0}-Entrada de impagats", _Banc.abrOrNom)
            DateTimePicker1.Value = DTO.GlobalVariables.Today()
            LoadGrid()
        End If
        _AllowEvents = True
    End Sub


    Private Sub LoadGrid()
        Dim oTb As New DataTable
        With oTb
            .Columns.Add("REF", System.Type.GetType("System.String"))
            .Columns.Add("EUR", System.Type.GetType("System.Decimal"))
            .Columns.Add("VTO", System.Type.GetType("System.DateTime"))
            .Columns.Add("TXT", System.Type.GetType("System.String"))
            .Columns.Add("NOM", System.Type.GetType("System.String"))
        End With

        Dim oRow As DataRow
        Dim oSum = DTOAmt.Empty
        For Each oImpagat As DTOImpagat In _Impagats
            oRow = oTb.NewRow
            oRow(Cols.Ref) = IIf(oImpagat.RefBanc > "", oImpagat.RefBanc, "")
            oRow(Cols.Eur) = oImpagat.Csb.Amt.Formatted
            oRow(Cols.Vto) = oImpagat.Csb.Vto
            oRow(Cols.Txt) = oImpagat.Csb.Txt
            oRow(Cols.Nom) = oImpagat.Csb.Contact.Nom
            oTb.Rows.Add(oRow)
            oSum.Add(oImpagat.Csb.Amt)
        Next

        Xl_AmountNominal.Amt = oSum

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            With .Columns(Cols.Ref)
                .HeaderText = "referencia"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "venciment"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "concepte"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "lliurat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oCca As DTOCca = Nothing
        UIHelper.ToggleProggressBar(Panel1, True)
        oCca = Await Update(oCca, exs)
        UIHelper.ToggleProggressBar(Panel1, False)
        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oCca))
            If CheckBoxWarnReps.Checked Then
                Await WarnReps(exs)
                If exs.Count = 0 Then
                    Me.Close()
                Else
                    UIHelper.WarnError(exs, "error al avisar als representants")
                End If
            End If
        Else
            UIHelper.WarnError(exs, "error al desar l'impagat")
        End If
    End Sub

    Private Shadows Async Function Update(oCca As DTOCca, exs As List(Of Exception)) As Task(Of DTOCca)
        Dim oAmtNominal As DTOAmt = Xl_AmountNominal.Amt.Clone
        Dim DtFch As Date = DateTimePicker1.Value

        For i As Integer = 0 To _Impagats.Count - 1
            _Impagats(i).RefBanc = DataGridView1.Rows(i).Cells(Cols.Ref).Value
        Next

        oCca = Await FEB.Impagats.Update(exs, _Impagats, _Banc, DtFch, oAmtNominal, oCca, Current.Session.User)

        Return oCca
    End Function


    Private Async Function WarnReps(exs As List(Of Exception)) As Task
        For Each oImpagat As DTOImpagat In _Impagats
            Await FEB.Impagat.WarnReps(exs, Current.Session.User, oImpagat)
        Next
    End Function



End Class
