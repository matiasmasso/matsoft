Imports Winforms

Public Class Frm_Last_Incidencies
    Private _Query As DTOIncidenciaQuery
    Private _AllowEvents As Boolean

    Public Sub New(oQuery As DTOIncidenciaQuery)
        MyBase.New()
        InitializeComponent()
        _Query = oQuery
    End Sub

    Private Async Sub Frm_Last_Incidencies_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await LoadComboboxClose()
        Dim years As New List(Of Integer)
        For i As Integer = Today.Year To 1985 Step -1
            years.Add(i)
        Next
        Xl_Years1.Load(years)

        With _Query
            If .Customer IsNot Nothing Then
                CheckBoxCustomer.Checked = True
                Xl_Contact21.Visible = True
                Xl_Contact21.Contact = .Customer
            End If
            If .Product IsNot Nothing Then
                CheckBoxProduct.Checked = True
                Xl_LookupProduct1.Visible = True
            End If
            Xl_LookupProduct1.Load(.Product, DTOProduct.SelectionModes.SelectAny)
            If .Tancament IsNot Nothing Then
                ComboBoxClose.SelectedItem = ComboBoxClose.DataSource.firstOrDefault(Function(x) x.Equals(.Tancament))
            End If
            Select Case .Src
                Case DTOIncidencia.Srcs.NotSet
                    CheckBoxSrcProducte.Checked = True
                    CheckBoxSrcTransport.Checked = True
                Case DTOIncidencia.Srcs.Producte
                    CheckBoxSrcProducte.Checked = True
                Case DTOIncidencia.Srcs.Transport
                    CheckBoxSrcTransport.Checked = True
            End Select
            CheckBoxIncludeClosed.Checked = .IncludeClosed
            ComboBoxClose.Enabled = CheckBoxIncludeClosed.Checked
        End With
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Application.DoEvents()
        Cursor = Cursors.WaitCursor
        With _Query
            .Src = GetSrc()
            .Product = GetProduct()
            .Customer = GetCustomer()
            .Tancament = GetTancament()
            .IncludeClosed = CheckBoxIncludeClosed.Checked
            .Year = Xl_Years1.Value
        End With
        ProgressBar1.Visible = True
        Dim oQuery = Await FEB2.Incidencias.Query(exs, _Query)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            _Query = oQuery
            Xl_Incidencies1.Load(_Query)
            Cursor = Cursors.Default
        End If
    End Function

    Private Function GetSrc() As DTOIncidencia.Srcs
        Dim retval As DTOIncidencia.Srcs = DTOIncidencia.Srcs.NotSet
        If CheckBoxSrcProducte.Checked And Not CheckBoxSrcTransport.Checked Then retval = DTOIncidencia.Srcs.Producte
        If CheckBoxSrcTransport.Checked And Not CheckBoxSrcProducte.Checked Then retval = DTOIncidencia.Srcs.Transport
        Return retval
    End Function

    Private Async Function LoadComboboxClose() As Task
        Dim exs As New List(Of Exception)
        Dim oCodisDeTancament = Await FEB2.Incidencias.CodisDeTancament(exs)
        If exs.Count = 0 Then
            Dim oDefaultCodi As New DTOIncidenciaCod(Guid.Empty)
            oDefaultCodi.Nom.Esp = "(tots els codis de tancament)"
            oCodisDeTancament.Insert(0, oDefaultCodi)

            With ComboBoxClose
                .DataSource = oCodisDeTancament
                .DisplayMember = "Esp"
                .SelectedIndex = 0
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Function



    Private Function GetProduct() As DTOProduct
        Dim retval As DTOProduct = Nothing
        If CheckBoxProduct.Checked Then
            retval = Xl_LookupProduct1.Product
        End If
        Return retval
    End Function

    Private Function GetCustomer() As DTOCustomer
        Dim retval As DTOCustomer = Nothing
        If CheckBoxCustomer.Checked Then
            If Xl_Contact21.Contact IsNot Nothing Then
                retval = New DTOCustomer(Xl_Contact21.Contact.Guid)
            End If
        End If
        Return retval
    End Function

    Private Function GetTancament() As DTOIncidenciaCod
        Dim retval As DTOIncidenciaCod = Nothing
        If ComboBoxClose.SelectedIndex > 0 Then
            retval = ComboBoxClose.SelectedItem
        End If
        Return retval
    End Function

    Private Async Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Contact21.AfterUpdate,
         Xl_LookupProduct1.AfterUpdate,
          ComboBoxClose.SelectedIndexChanged,
           CheckBoxSrcProducte.CheckedChanged,
            CheckBoxSrcTransport.CheckedChanged

        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Async Sub CheckBoxProduct_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxProduct.CheckedChanged
        If _AllowEvents Then
            Xl_LookupProduct1.Visible = CheckBoxProduct.Checked
            If Not CheckBoxProduct.Checked Then Await refresca()
        End If
    End Sub

    Private Async Sub CheckBoxCustomer_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCustomer.CheckedChanged
        If _AllowEvents Then
            Xl_Contact21.Visible = CheckBoxCustomer.Checked
            If Not CheckBoxCustomer.Checked Then
                Xl_Contact21.Contact = Nothing
                Await refresca()
            End If
        End If
    End Sub

    Private Async Sub CheckBoxIncludeClosed_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIncludeClosed.CheckedChanged
        If _AllowEvents Then
            ComboBoxClose.Enabled = CheckBoxIncludeClosed.Checked
            Xl_Years1.Visible = CheckBoxIncludeClosed.Checked
            Await refresca()
        End If
    End Sub

    Private Async Sub Xl_Incidencies1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Incidencies1.RequestToRefresh
        Await refresca()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = FEB2.IncidenciaQuery.ExcelSheet(_Query, Application.CurrentCulture)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Async Sub ReposicionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReposicionsToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Cursor = Cursors.WaitCursor
        Dim items = Await FEB2.Incidencias.Reposicions(exs, Current.Session.Emp, Today.Year - 1)
        If exs.Count = 0 Then
            For Each item As DTOIncidencia In items
                item.Url = FEB2.UrlHelper.Factory(True, "incidencia", item.Guid.ToString())
            Next
            Dim oSheet = DTOIncidencia.ExcelReposicions(items)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
            Cursor = Cursors.Default
        Else
            Cursor = Cursors.Default
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Await refresca()
    End Sub
End Class