
Public Class DTORol

    Property id As Ids

    Property nom As DTOLangText

    Public Property dsc As String

    Property isLoaded As Boolean
    Property isNew As Boolean

    Public Enum Ids
        NotSet
        SuperUser
        Admin
        Operadora
        Accounts
        SalesManager
        Comercial
        Taller
        Rep = 8
        CliFull = 9
        Manufacturer = 10
        Marketing = 11
        CliLite = 12
        Auditor = 14
        Banc = 15
        Transportista = 16
        LogisticManager = 17
        PR = 18
        Guest = 20
        Lead = 98
        Unregistered = 99
        Denied = 100
    End Enum

    Public Sub New()
        MyBase.New()
        _Id = Ids.Unregistered
        _Nom = New DTOLangText
    End Sub

    Public Sub New(oId As Ids)
        MyBase.New()
        _Id = oId
        _Nom = New DTOLangText
    End Sub

    Public ReadOnly Property NomEsp As String
        Get
            Return _Nom.Esp
        End Get
    End Property

    Public Function IsAuthenticated() As Boolean
        Dim retval As Boolean
        Select Case _Id
            Case DTORol.Ids.NotSet, DTORol.Ids.Unregistered, DTORol.Ids.Denied
            Case Else
                retval = True
        End Select
        Return retval
    End Function


    Public Function IsSuperAdmin() As Boolean
        Dim Retval As Boolean = False
        Select Case _Id
            Case Ids.SuperUser
                Retval = True
        End Select
        Return Retval
    End Function

    Public Function IsAdmin() As Boolean
        Dim Retval As Boolean = False
        Select Case _Id
            Case Ids.SuperUser, Ids.Admin
                Retval = True
        End Select
        Return Retval
    End Function

    Public Function IsMainboard() As Boolean
        Dim Retval As Boolean = False
        Select Case _Id
            Case Ids.SuperUser, Ids.Admin, Ids.SalesManager
                Retval = True
        End Select
        Return Retval
    End Function

    Public Function IsStaff(Optional ByVal oRolId As Ids = Ids.NotSet) As Boolean
        If oRolId = Ids.NotSet Then oRolId = _Id

        Dim oRetVal As Boolean = False
        Select Case oRolId
            Case Ids.SuperUser,
            Ids.Admin,
            Ids.SalesManager,
             Ids.Accounts,
              Ids.LogisticManager,
               Ids.Marketing,
               Ids.Operadora,
                Ids.Comercial,
                 Ids.Taller

                oRetVal = True
        End Select
        Return oRetVal
    End Function

    Public Function IsRep(Optional ByVal oRolId As Ids = Ids.NotSet) As Boolean
        If oRolId = Ids.NotSet Then oRolId = _Id

        Dim oRetVal As Boolean = False
        Select Case oRolId
            Case Ids.Rep,
            Ids.Comercial

                oRetVal = True
        End Select

        Return oRetVal
    End Function

    Public Function IsProfesional() As Boolean
        Dim retval As Boolean
        Select Case _Id
            Case Ids.Denied, Ids.Guest, Ids.Lead, Ids.NotSet, Ids.Unregistered
            Case Else
                retval = True
        End Select
        Return retval
    End Function

    Public Shadows Function Equals(oCandidate As Object) As Boolean
        Dim retval As Boolean
        If oCandidate IsNot Nothing Then
            If TypeOf oCandidate Is DTORol Then
                retval = (oCandidate.id = _Id)
            End If
        End If
        Return retval
    End Function
End Class
