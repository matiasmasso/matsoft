Public Class DTOBankBranch
    Inherits DTOBaseGuid

    Property bank As DTOBank
    Property id As String
    Property location As DTOLocation
    Property address As String
    Property swift As String
    Property tel As String
    Property obsoleto As Boolean

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oBank As DTOBank) As DTOBankBranch
        Dim retval As New DTOBankBranch
        With retval
            .Bank = oBank
        End With
        Return retval
    End Function

    Shared Function FullNomAndAddress(oBranch As DTOBankBranch) As String
        Dim sb As New System.Text.StringBuilder
        If oBranch IsNot Nothing Then
            sb.Append(DTOBank.NomComercialORaoSocial(oBranch.Bank))
            If oBranch.Location IsNot Nothing Then
                sb.Append(" - ")
                sb.Append(oBranch.Location.Nom)
                If oBranch.Address > "" Then
                    sb.Append(" - ")
                    sb.Append(oBranch.Address)
                End If
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function FullNomAndAddressHtml(oBranch As DTOBankBranch) As String
        Dim sb As New System.Text.StringBuilder
        If oBranch IsNot Nothing Then
            sb.Append(DTOBank.NomComercialORaoSocial(oBranch.Bank))
            If oBranch.Location IsNot Nothing Then
                If oBranch.Address > "" Then
                    sb.Append("<br/>")
                    sb.Append(oBranch.Address)
                End If
                sb.Append("<br/>")
                sb.Append(oBranch.Location.Nom)
            End If
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

End Class


