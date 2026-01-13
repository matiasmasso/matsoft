Public Class Frm_Product_Historial
    Private _Product As DTOProduct
    Private _Values As List(Of DTODeliveryItem)
    Private _Allowevents As Boolean

    Public Sub New(oProduct As DTOProduct)
        MyBase.New
        InitializeComponent()
        _Product = oProduct
    End Sub

    Private Async Sub Frm_Product_Historial_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Product.Load(_Product, exs) Then
            If TypeOf _Product Is DTOProductSku Then
                Me.Text = String.Format("Historial de ({0}) {1}", DirectCast(_Product, DTOProductSku).Id, DTOProduct.GetNom(_Product))
            Else
                Me.Text = String.Format("Historial de {0}", DTOProduct.GetNom(_Product))
            End If

            ProgressBar1.Visible = True
            _Values = Await FEB2.DeliveryItems.All(exs, _Product)
            ProgressBar1.Visible = False

            If exs.Count = 0 Then
                _Values = _Values.OrderByDescending(Function(x) x.Delivery.Id).OrderByDescending(Function(x) x.Delivery.Fch).ToList
                LoadMgzs()
                refresca()
                _Allowevents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refresca()
        Dim oCurrentMgz As DTOMgz = CurrentMgz()

        Dim oMgzValues As List(Of DTODeliveryItem)
        If oCurrentMgz Is Nothing Then
            oMgzValues = _Values.Where(Function(x) x.Mgz Is Nothing).ToList
        Else
            oMgzValues = _Values.Where(Function(x) x.Mgz.Equals(oCurrentMgz)).ToList
        End If

        Xl_Product_Historial1.Load(Current.Session.User, oMgzValues, CurrentMode)
    End Sub

    Private Sub LoadMgzs()
        Dim oMgzs = _Values.Where(Function(m) m.Mgz IsNot Nothing).GroupBy(Function(x) x.Mgz.Guid).Select(Function(y) y.First).Select(Function(z) z.Mgz).OrderBy(Function(q) q.Abr).ToList

        If _Values.Any(Function(x) x.Delivery.Mgz Is Nothing) Then
            Dim oNoMgz As New DTOMgz(Guid.Empty)
            oNoMgz.Abr = "(no especificat)"
            oMgzs.Add(oNoMgz)
        End If

        With ComboBoxMgz
            .DataSource = oMgzs
            .DisplayMember = "Abr"
            .SelectedItem = oMgzs.FirstOrDefault(Function(x) x.Equals(GlobalVariables.Emp.Mgz))
        End With
    End Sub

    Private Function CurrentMgz() As DTOMgz
        Dim retval As DTOMgz = ComboBoxMgz.SelectedItem
        Return retval
    End Function

    Private Function CurrentMode()
        Dim retval As Xl_ProductHistorial.Modes = Xl_ProductHistorial.Modes.All
        If Not CheckBoxInp.Checked Then
            retval = Xl_ProductHistorial.Modes.Sortides
        ElseIf Not CheckBoxOut.Checked Then
            retval = Xl_ProductHistorial.Modes.Entrades
        End If
        Return retval
    End Function

    Private Sub ComboBoxMgz_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxMgz.SelectedIndexChanged
        If _Allowevents Then
            refresca()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxInp.CheckedChanged
        If _Allowevents Then
            If CheckBoxInp.Checked = False Then CheckBoxOut.Checked = True
            refresca()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOut.CheckedChanged
        If _Allowevents Then
            If CheckBoxOut.Checked = False Then CheckBoxInp.Checked = True
            refresca()
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Product_Historial1.Filter = e.Argument
    End Sub

    Private Sub Xl_Product_Historial1_RequestForExcel(sender As Object, e As MatEventArgs) Handles Xl_Product_Historial1.RequestForExcel
        Dim oFilteredValues As List(Of DTODeliveryItem) = Xl_Product_Historial1.FilteredValues()
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = DTODeliveryItem.Excel(oFilteredValues, Me.Text)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class