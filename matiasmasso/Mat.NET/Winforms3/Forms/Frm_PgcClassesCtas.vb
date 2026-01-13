Public Class Frm_PgcClassesCtas
    Private Sub Frm_Class_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Xl_LookupPgcPlan1.PgcPlan = DTOApp.Current.PgcPlan
        RefrescaClasses(Me, MatEventArgs.Empty)
    End Sub

    Private Async Function NullCtasClass() As Task(Of DTOPgcClass)
        Dim exs As New List(Of Exception)
        Dim oPlan As DTOPgcPlan = DTOApp.Current.PgcPlan
        Dim retval As DTOPgcClass = Nothing

        Dim oCtas = Await FEB.PgcCtas.All(exs, oPlan, retval)
        If oCtas.Count > 0 Then
            retval = New DTOPgcClass(Guid.Empty)
            With retval
                .Nom = New DTOLangText("Cuentas pendientes de clasificar", "Comptes pendents de classificar", "Missing class accounts")
            End With
        End If
        Return retval
    End Function

    Private Async Sub Xl_PgcClassTree1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_PgcClassTree1.ValueChanged
        Dim oClass As DTOPgcClass = e.Argument
        Await RefrescaCtas(oClass)
    End Sub

    Private Async Sub Xl_PgcClassTree1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PgcClassTree1.RequestToRefresh
        RefrescaClasses(Me, MatEventArgs.Empty)
        Dim oClass As DTOPgcClass = e.Argument
        If oClass Is Nothing Then
            oClass = Xl_PgcClassTree1.SelectedClass
        End If
        Await RefrescaCtas(oClass)
    End Sub

    Private Async Sub RefrescaClasses(sender As Object, e As MatEventArgs)
        Dim oPlan As DTOPgcPlan = Xl_LookupPgcPlan1.PgcPlan
        Dim exs As New List(Of Exception)
        Dim oAllClasses = Await FEB.PgcClasses.All(exs, oPlan)
        If exs.Count = 0 Then
            Dim oTree As List(Of DTOPgcClass) = DTOPgcClass.Tree(oAllClasses)
            Dim oNullCtasClass = Await NullCtasClass()
            If oNullCtasClass IsNot Nothing Then oTree.Add(oNullCtasClass)
            Xl_PgcClassTree1.Load(oTree, Current.Session.User.Lang, e.Argument)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub RefrescaCtas(sender As Object, e As MatEventArgs)
        Dim oCta As DTOPgcCta = e.Argument
        Dim oClass As DTOPgcClass = oCta.PgcClass
        Await RefrescaCtas(oClass)
    End Sub

    Private Async Function RefrescaCtas(oClass As DTOPgcClass) As Task
        Dim exs As New List(Of Exception)
        Dim oPlan As DTOPgcPlan = Xl_LookupPgcPlan1.PgcPlan
        Dim oCtas As List(Of DTOPgcCta) = Nothing
        If oClass IsNot Nothing Then
            If oClass.Guid.Equals(Guid.Empty) Then
                'empty class ctas
                Dim oNullClass As DTOPgcClass = Nothing
                oCtas = Await FEB.PgcCtas.All(exs, oPlan, oNullClass)
            Else
                oCtas = Await FEB.PgcCtas.All(exs, oPlan, oClass)
            End If

            If exs.Count = 0 Then
                Xl_PgcCtas1.Load(oCtas)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Sub Xl_PgcClassTree1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PgcClassTree1.RequestToAddNew
        Dim oParent As DTOPgcClass = Xl_PgcClassTree1.SelectedClass
        Dim iOrdinal As Integer
        If oParent.Children.Count > 0 Then
            Dim oLastChild As DTOPgcClass = oParent.Children.Last
            iOrdinal = oLastChild.Ord + 1
        End If
        Dim oPgcClass As New DTOPgcClass
        With oPgcClass
            .Parent = oParent
            .Plan = oParent.Plan
            .Ord = iOrdinal
        End With
        Dim oFrm As New Frm_PgcClass(oPgcClass)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaClasses
        oFrm.Show()

    End Sub

    Private Sub Xl_PgcClassTree1_RequestToAddRoot(sender As Object, e As MatEventArgs) Handles Xl_PgcClassTree1.RequestToAddRoot
        Dim oPgcClass As New DTOPgcClass
        oPgcClass.Plan = Xl_LookupPgcPlan1.PgcPlan
        Dim oFrm As New Frm_PgcClass(oPgcClass)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaClasses
        oFrm.Show()
    End Sub

    Private Async Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Dim exs As New List(Of Exception)
        Dim oPlan As DTOPgcPlan = DTOApp.Current.PgcPlan
        Dim oCtas = Await FEB.PgcCtas.SearchAsync(oPlan, TextBoxSearch.Text, exs)
        If exs.Count = 0 Then
            Xl_PgcCtas1.Load(oCtas)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_PgcCtas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PgcCtas1.RequestToAddNew
        Dim oClass As DTOPgcClass = Xl_PgcClassTree1.SelectedClass
        If oClass Is Nothing Then
            UIHelper.WarnError("Cal sel·leccionar l'epigraf a la esquerra on volem obrir el compte")
        Else
            Dim oRootClass = DTOPgcClass.Root(oClass)
            Dim oCta As New DTOPgcCta
            With oCta
                .Plan = DTOApp.Current.PgcPlan
                .PgcClass = Xl_PgcClassTree1.SelectedClass
                Select Case oRootClass.Cod
                    Case DTOPgcClass.Cods.aA_Activo
                        .Act = True
                End Select
            End With
            Dim oFrm As New Frm_PgcCta(oCta)
            AddHandler oFrm.AfterUpdate, AddressOf RefrescaCtas
            oFrm.Show()
        End If
    End Sub

    Private Async Sub Xl_PgcCtas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PgcCtas1.RequestToRefresh
        Dim oCta As DTOPgcCta = e.Argument
        Dim oClass As DTOPgcClass = oCta.PgcClass
        Await RefrescaCtas(oClass)
    End Sub
End Class