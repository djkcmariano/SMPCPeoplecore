Imports Microsoft.VisualBasic

Public Class clsPWDComplexity
    Public Shared Function clsPasswordFormat() As String
        Return "^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=*.!/]).*$"
    End Function
    'Public Shared Function clsPasswordFormatRepeated() As String
    '    Return "^.*(?=.*[a,a,a])(?=.*[AAA])(?=.*[bbb])(?=.*[BBB])(?=.*[ccc])(?=.*[CCC])(?=.*[ddd])(?=.*[DDD])(?=.*[eee])(?=.*[EEE])(?=.*[fff])(?=.*[FFF])(?=.*[ggg])(?=.*[GGG])(?=.*[hhh])(?=.*[HHH])(?=.*[iii])(?=.*[III])(?=.*[jjj])(?=.*[JJJ])(?=.*[kkk])(?=.*[KKK])(?=.*[lll])(?=.*[LLL])(?=.*[mmm])(?=.*[MMM])(?=.*[nnn])(?=.*[NNN])(?=.*[ppp])(?=.*[PPP])(?=.*[qqq])(?=.*[QQQ])(?=.*[rrr])(?=.*[RRR])(?=.*[sss])(?=.*[SSS])(?=.*[ttt])(?=.*[TTT])(?=.*[uuu])(?=.*[UUU])(?=.*[vvv])(?=.*[VVV])(?=.*[www])(?=.*[WWW])(?=.*[xxx])(?=.*[XXX])(?=.*[yyy])(?=.*[YYY])(?=.*[zzz])(?=.*[ZZZ])(?=.*[111])(?=.*[222])(?=.*[333])(?=.*[666])(?=.*[444])(?=.*[555])(?=.*[777])(?=.*[888])(?=.*[999])(?=.*[000]).*$"
    'End Function
    Public Shared Function clsPasswordFormatRepeated() As String
        Return "^.*(aaa|AAA|bbb|BBB|ccc|CCC|ddd|DDD|eee|EEE|fff|FFF|ggg|GGG|hhh|HHH|iii|III|jjj|JJJ|kkk|KKK|lll|LLL|mmm|MMM|nnn|NNN|ooo|OOO|ppp|PPP|qqq|QQQ|rrr|RRR|sss|SSS|ttt|TTT|uuu|UUU|vvv|VVV|xxx|XXX|yyy|YYY|zzz|ZZZ|000|111|222|333|444|555|666|777|888|999).*$"

    End Function

    'Public Shared Function clsPasswordFormatAscending() As String
    '    Return "^.*(?=.*[abc])(?=.*[ABC])(?=.*[bcd])(?=.*[BCD])(?=.*[cde])(?=.*[CDE])(?=.*[efg])(?=.*[EFG])(?=.*[fgh])(?=.*[FGH])(?=.*[ghi])(?=.*[GHI])(?=.*[hij])(?=.*[HIJ])(?=.*[ijk])(?=.*[IJK])(?=.*[jkl])(?=.*[JKL])(?=.*[klm])(?=.*[KLM])(?=.*[lmn])(?=.*[LMN])(?=.*[mno])(?=.*[MNO])(?=.*[nop])(?=.*[NOP])(?=.*[opq])(?=.*[OPQ])(?=.*[pqr])(?=.*[PQR])(?=.*[qrs])(?=.*[QRS])(?=.*[rst])(?=.*[RST])(?=.*[stu])(?=.*[STU])(?=.*[tuv])(?=.*[TUV])(?=.*[uvw])(?=.*[UVW])(?=.*[vwx])(?=.*[VWX])(?=.*[wxy])(?=.*[WXY])(?=.*[xyz])(?=.*[XYZ])(?=.*[yza])(?=.*[YZA])(?=.*[zab])(?=.*[ZAB])(?=.*[123])(?=.*[234])(?=.*[345])(?=.*[456])(?=.*[567])(?=.*[678])(?=.*[789])(?=.*[890])(?=.*[901])(?=.*[012]).*$"
    'End Function
    Public Shared Function clsPasswordFormatAscending() As String
        Return "^.*(abc|ABC|bcd|BCD|cde|CDE|def|DEF|efg|EFG|fgh|FGH|ghi|GHI|hij|HIJ|ijk|IJK|jkl|JKL|klm|KLM|lmn|LMN|mno|MNO|nop|NOP|opq|OPQ|pqr|PQR|qrs|QRS|rst|RST|stu|STU|tuv|TUV|uvw|UVW|vwx|VWX|wxy|WXY|xyz|XYZ|123|234|345|456|567|678|789|890|901|012).*$"

    End Function

    'Public Shared Function clsPasswordFormatDescending() As String
    '    Return "^.*(?=.*[987])(?=.*[876])(?=.*[765])(?=.*[654])(?=.*[543])(?=.*[432])(?=.*[321])(?=.*[210])(?=.*[109])(?=.*[098])(?=.*[zyx])(?=.*[ZYX])(?=.*[yxw])(?=.*[YXW])(?=.*[xwv])(?=.*[XWV])(?=.*[wvu])(?=.*[WVU])(?=.*[vut])(?=.*[VUT])(?=.*[uts])(?=.*[UTS])(?=.*[tsr])(?=.*[TSR])(?=.*[srq])(?=.*[SRQ])(?=.*[rqp])(?=.*[RQP])(?=.*[qpo])(?=.*[QPO])(?=.*[pon])(?=.*[PON])(?=.*[onm])(?=.*[ONM])(?=.*[nml])(?=.*[NML])(?=.*[mlk])(?=.*[MLK])(?=.*[lkj])(?=.*[LKJ])(?=.*[kji])(?=.*[KJI])(?=.*[jih])(?=.*[JIH])(?=.*[ihg])(?=.*[IHG])(?=.*[hgf])(?=.*[HGF])(?=.*[gfe])(?=.*[GFE])(?=.*[fed])(?=.*[FED])(?=.*[edc])(?=.*[EDC])(?=.*[dcb])(?=.*[DCB])(?=.*[cba])(?=.*[CBA])(?=.*[baz])(?=.*[BAZ]).*$"
    'End Function
    Public Shared Function clsPasswordFormatDescending() As String
        Return "^.*(987|876|765|654|543|432|321|210|109|098|zxy|ZXY|yxw|YXW|xwv|XWV|wvu|WVU|vut|VUT|uts|UTS|tsr|TSR|srq|SRQ|rqp|RQP|qpo|QPO|pon|PON|onm|ONM|nml|NML|mlk|MLK|lkj|LKJ|kji|KJI|jih|JIH|ihg|IHG|hgf|HGF|gfe|GFE|fed|FED|edc|EDC|dcb|DCB|cba|CBA).*$"

    End Function


End Class
