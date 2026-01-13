
Imports System.Data.SqlClient

Public Class Xl_Rol_Usrs
    Private Enum Cols
        Id
        Nom
    End Enum

    Public Event AfterUpdate()

    Private mDs As DataSet
    Private mBinded As Boolean
    Private mContacts As Contacts
    Private mRol As DTORol

    Public Property Rol() As DTORol
        Get
            Return mRol
        End Get
        Set(ByVal Value As DTORol)
            mRol = Value
            If Not Value Is Nothing Then
                If Not mBinded Then LoadGrid()
            End If
        End Set
    End Property

    Public ReadOnly Property Contacts() As Contacts
        Get
            Return GetItms()
        End Get
    End Property

    Private Function GetItms() As Contacts
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim oContacts As New Contacts
        Dim oContact As Contact
        Dim ContactId As Integer
        For Each oRow In oTb.Rows
            ContactId = oRow(Cols.Id)
            oContact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, ContactId)
            oContacts.Add(oContact)
        Next
        Return oContacts
    End Function

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Cli, RaoSocial " _
        & "FROM CliGral " _
        & "WHERE emp =" & App.Current.Emp.Id & " AND " _
        & "ROL=" & mRol.Id() & " " _
        & "ORDER BY RAOSOCIAL"
        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        mBinded = True
    End Sub

    Private Function CurrentContact() As Contact
        Dim oContact As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim ContactId As Integer = oRow.Cells(Cols.Id).Value
            oContact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, ContactId)
        End If
        Return oContact
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        root.ShowContact(CurrentContact)
    End Sub
End Class
