Public Class Xl_RegistresJornadesLaborals
    Inherits TreeView

    Private _value As Models.JornadesLaboralsModel
    Private _selectedStaff As Models.JornadesLaboralsModel.Staff
    Private _selectedFch As Date
    Private _selectedItem As Models.JornadesLaboralsModel.Item

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Sub Load(value As Models.JornadesLaboralsModel)
        _value = value
        Dim years = _value.Years()
        MyBase.Nodes.Clear()

        For Each year As Integer In years.OrderByDescending(Function(x) x).ToList()
            Dim oNodeYear = NodeYear(year)
            MyBase.Nodes.Add(oNodeYear)
            If year = years.First Then oNodeYear.Expand()
            Dim staffs = _value.Staffs.Where(Function(x) x.Items.Any(Function(y) y.FchFrom.Year = year)).ToList()
            For Each staff In staffs
                Dim staffItems = staff.Items.Where(Function(x) x.FchFrom.Year = year).ToList()
                Dim oNodeStaff = NodeStaff(staff, staffItems)
                If _selectedStaff IsNot Nothing AndAlso _selectedStaff.Guid.Equals(staff.Guid) Then oNodeStaff.EnsureVisible()
                oNodeYear.Nodes.Add(oNodeStaff)
                Dim months = staffItems.GroupBy(Function(x) x.FchFrom.Month).Select(Function(y) y.First()).Select(Function(z) z.FchFrom.Month).ToList()
                For Each month As Integer In months
                    Dim monthItems = staffItems.Where(Function(x) x.FchFrom.Month = month).ToList()
                    Dim oNodeMonth = NodeMonth(monthItems)
                    oNodeStaff.Nodes.Add(oNodeMonth)
                    Dim days = monthItems.GroupBy(Function(x) x.FchFrom.Day).Select(Function(y) y.First()).Select(Function(z) z.FchFrom.Day).ToList()
                    For Each day In days
                        Dim dayItems = monthItems.Where(Function(x) x.FchFrom.Day = day).ToList()
                        Dim oNodeDay = NodeDay(dayItems)
                        oNodeMonth.Nodes.Add(oNodeDay)
                        For Each item In dayItems
                            Dim oNodeItem = NodeItem(item)
                            oNodeDay.Nodes.Add(oNodeItem)
                            If _selectedItem IsNot Nothing AndAlso _selectedItem.Guid.Equals(item.Guid) Then oNodeItem.EnsureVisible()
                        Next
                    Next
                Next
            Next
        Next

        If MyBase.Nodes.Count > 0 Then
            MyBase.Nodes(0).Expand()
        End If
    End Sub

    Private Function NodeYear(year As Integer) As TreeNode
        Dim retval As New TreeNode(year.ToString)
        Return retval
    End Function

    Private Function NodeStaff(staff As Models.JornadesLaboralsModel.Staff, items As List(Of Models.JornadesLaboralsModel.Item)) As TreeNode
        Dim horas = items.Sum(Function(y) y.Horas())
        Dim caption = String.Format("{0}: {1:N2} horas", staff.Abr, horas)
        Dim retval As New TreeNode(caption)
        retval.Tag = staff
        Return retval
    End Function

    Private Function NodeMonth(items As List(Of Models.JornadesLaboralsModel.Item)) As TreeNode
        Dim month = items.First().FchFrom.Month
        Dim horas = items.Sum(Function(y) y.Horas())
        Dim caption = String.Format("{0}: {1:N2} horas", DTOLang.ESP().MesAbr(month), horas)
        Dim retval As New TreeNode(caption)
        Return retval
    End Function

    Private Function NodeDay(items As List(Of Models.JornadesLaboralsModel.Item)) As TreeNode
        Dim fch = items.First().FchFrom.Date()
        Dim horas = items.Sum(Function(x) x.Horas)
        Dim caption As String = String.Format("día {0:00} {1}", fch.Day, DTOLang.ESP().WeekDay(fch))
        If horas > 0 Then caption = String.Format("{0}: {1:N2} horas", caption, horas)
        Dim retval As New TreeNode(caption)
        retval.Tag = fch
        Return retval
    End Function

    Private Function NodeItem(item As Models.JornadesLaboralsModel.Item) As TreeNode
        Dim caption As String = ""
        If item.IsOpen() Then
            caption = String.Format("{0} -", item.FchFrom.ToString("HH:mm"))
        Else
            caption = String.Format("{0} - {1}: {2} horas", item.FchFrom.ToString("HH:mm"), item.FchTo.ToString("HH:mm"), item.Horas)
        End If
        Dim retval As New TreeNode(caption)
        retval.Tag = item
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            If oNode.Tag IsNot Nothing Then
                If TypeOf oNode.Tag Is Models.JornadesLaboralsModel.Item Then
                    Dim item As Models.JornadesLaboralsModel.Item = oNode.Tag
                    Dim oJornadaLaboral As New DTOJornadaLaboral(item.Guid)
                    Dim oMenu_JornadaLaboral As New Menu_JornadaLaboral(oJornadaLaboral)
                    AddHandler oMenu_JornadaLaboral.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_JornadaLaboral.Range)
                    oContextMenu.Items.Add("Afegir", Nothing, AddressOf AddItem)
                ElseIf TypeOf oNode.Tag Is Date Then
                    oContextMenu.Items.Add("Afegir", Nothing, AddressOf AddItem)
                ElseIf TypeOf oNode.Tag Is Models.JornadesLaboralsModel.Staff Then
                    Dim staff As Models.JornadesLaboralsModel.Staff = oNode.Tag
                    Dim oStaff As New DTOStaff(staff.Guid)
                    Dim oMenu_Staff As New Menu_Staff(oStaff)
                    oContextMenu.Items.AddRange(oMenu_Staff.Range)
                    oContextMenu.Items.Add("Afegir", Nothing, AddressOf AddItem)
                End If
            End If
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub AddItem(sender As Object, e As EventArgs)
        Dim oNode = MyBase.SelectedNode
        If oNode IsNot Nothing Then
            If oNode.Tag IsNot Nothing Then
                If TypeOf oNode.Tag Is Models.JornadesLaboralsModel.Item Then
                    Dim oFchNode = oNode.Parent
                    Dim oMesNode = oFchNode.Parent
                    Dim oStaffNode = oMesNode.Parent
                    Dim staff As Models.JornadesLaboralsModel.Staff = oStaffNode.Tag
                    Dim oJornadaLaboral = New DTOJornadaLaboral()
                    oJornadaLaboral.Staff = New DTOStaff(staff.Guid)
                    oJornadaLaboral.Staff.Abr = staff.Abr
                    oJornadaLaboral.FchFrom = oFchNode.Tag
                    Dim oFrm As New Frm_JornadaLaboral(oJornadaLaboral)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                ElseIf TypeOf oNode.Tag Is Date Then
                    Dim oFchNode = oNode
                    Dim oMesNode = oFchNode.Parent
                    Dim oStaffNode = oMesNode.Parent
                    Dim staff As Models.JornadesLaboralsModel.Staff = oStaffNode.Tag
                    Dim oJornadaLaboral = New DTOJornadaLaboral()
                    oJornadaLaboral.Staff = New DTOStaff(staff.Guid)
                    oJornadaLaboral.Staff.Abr = staff.Abr
                    oJornadaLaboral.FchFrom = oFchNode.Tag
                    Dim oFrm As New Frm_JornadaLaboral(oJornadaLaboral)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                ElseIf TypeOf oNode.Tag Is Models.JornadesLaboralsModel.Staff Then
                    Dim oStaffNode = oNode
                    Dim staff As Models.JornadesLaboralsModel.Staff = oStaffNode.Tag
                    Dim oJornadaLaboral = New DTOJornadaLaboral()
                    oJornadaLaboral.Staff = New DTOStaff(staff.Guid)
                    oJornadaLaboral.Staff.Abr = staff.Abr
                    oJornadaLaboral.FchFrom = DTO.GlobalVariables.Now()
                    Dim oFrm As New Frm_JornadaLaboral(oJornadaLaboral)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                End If
            End If
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, New MatEventArgs)
    End Sub

    Public Function Excel() As MatHelper.Excel.Book
        Return _value.Excel()
    End Function

    Private Sub Xl_RegistresJornadesLaborals_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        Dim oNode = MyBase.SelectedNode
        If oNode IsNot Nothing Then
            If oNode.Tag IsNot Nothing Then
                If TypeOf oNode.Tag Is Models.JornadesLaboralsModel.Item Then
                    Dim oFchNode = oNode.Parent
                    Dim oMesNode = oFchNode.Parent
                    Dim oStaffNode = oMesNode.Parent
                    _selectedStaff = oStaffNode.Tag
                    _selectedItem = oNode.Tag
                ElseIf TypeOf oNode.Tag Is Date Then
                    Dim oFchNode = oNode
                    Dim oMesNode = oFchNode.Parent
                    Dim oStaffNode = oMesNode.Parent
                    _selectedStaff = oStaffNode.Tag
                ElseIf TypeOf oNode.Tag Is Models.JornadesLaboralsModel.Staff Then
                    Dim oStaffNode = oNode
                    Dim staff As Models.JornadesLaboralsModel.Staff = oStaffNode.Tag
                    _selectedStaff = oStaffNode.Tag
                End If
            End If
        End If

        SetContextMenu()
    End Sub
End Class
