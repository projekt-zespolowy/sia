SQLite format 3   @    c�          
  

                                                         �    	��z�                                                                                                                                                                                                                                                                  ��tablekuponkuponCREATE TABLE kupon
(
	koduz INTEGER NOT NULL,
	kodwyd INTEGER NOT NULL,
	kodwyn INTEGER NOT NULL,
	stawka INTEGER NOT NULL
)��tablewynikwynikCREATE TABLE wynik
(
	kodwyd INTEGER NOT NULL,
	kodwyn INTEGER NOT NULL,
	nazwawyn VARCHAR,
	PRIMARY KEY (kodwyd, kodwyn)
))= indexsqlite_autoindex_wynik_1wynik   �!!�ktablewydarzeniewydarzenieCREATE TABLE wydarzenie
(
	kodwyd INTEGER NOT NULL,
	nazwawyd VARCHAR NOT NULL,
	ryzyko INTEGER NOT NULL
)�
!!�_tableuzytkownikuzytkownikCREATE TABLE uzytkownik
(
	koduz INTEGER NOT NULL,
	konto INTEGER NOT NULL,
	imie VARCHAR NOT NU   � � ���                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         �SYSTEM��Wojtek   z�Koz	�}�Kozak   � �����                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Popularny��!Egzotycznyu0Konklawe@	Finał@	NULLEVENT   - ��������qcME=5-                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       	BA	BA)Inny kardynał
Ortega	BettoriAmatoBertoneTurkson
	ScolaBergoglio	)Bayern Munchen		(REMIS)	/Borussia Dortmund
   � ���������������                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              		
							   H � ���������������{pdXL@4(�����������vj^TH<0$ ����������sg[OC7+ � � � � � � � � � � 
N	���
M	���
L	<
K	)�
J	 /

H	�#	G	nC
F	��
E	�I9
D	���
C	���
B	 Ȯ
A	:
@	?4
?	 /
>	���
=	���
<	��H	;	SR
:	?
9	
%�
8	���
7	��
6	���
5	<4	�
3	 /
2	���
1	���
0	���
/	<
.	;
-	 .
,	���
+	���
*	���)	�
(	?
'	?
&	
ML
%	���
$	���
#	���"	�
!	m�
 	?
	
MK
	�V
	���
	���
	���
	<
	;
	 .
	���
	���
	���
	��
	;
	 .		JX			
						    3  3 �S�� /                            �
!!�_tableuzytkownikuzytkownikCREATE TABLE uzytkownik
(
	koduz INTEGER NOT NULL,
	konto INTEGER NOT NULL,
	imie VARCHAR NOT NULL
)�!!�ktablewydarzeniewydarzenieCREATE TABLE wydarzenie
(
	kodwyd INTEGER NOT NULL,
	nazwawyd VARCHAR NOT NULL,
	ryzyko INTEGER NOT NULL
)��tablewynikwynikCREATE TABLE wynik
(
	kodwyd INTEGER NOT NULL,
	kodwyn INTEGER NOT NULL,
	nazwawyn VARCHAR,
	PRIMARY KEY (kodwyd, kodwyn)
))= indexsqlite_autoindex_wynik_1wynik��tablekuponkuponCREATE TABLE kupon
(
	koduz INTEGER NOT NULL,
	kodwyd INTEGER NOT NULL,
	kodwyn INTEGER NOT NULL,
	stawka INTEGER NOT NULL
)�H�kviewkuponagkuponagCREATE VIEW kuponag AS 
	SELECT 
		kupon.koduz, kupon.kodwyd, kupon.kodwyn,
		nazwawyd, nazwawyn,
		sum(stawka) AS stawka
	FROM kupon NATURAL JOIN wynik NATURAL JOIN wydarzenie
	GROUP BY kupon.koduz, kupon.kodwyd, kupon.kodwyn, nazwawyd, nazwawyn
	ORDER BY kupon.koduz, kupon.kodwyd, kupon.kodwyn       	                        
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 a�viewrynekrynekCREATE VIEW rynek AS
	SELECT
		kupon.kodwyd, kupon.kodwyn,
		nazwawyd, nazwawyn,
		sum(stawka) AS stawka
	FROM wynik LEFT OUTER JOIN kupon ON (kupon.kodwyd = wynik.kodwyd AND kupon.kodwyn = wynik.kodwyn) NATURAL JOIN wydarzenie 
	GROUP BY kupon.kodwyd, kupon.kodwyn, nazwawyd, nazwawyn
	ORDER BY kupon�Y	   
	   � ���������������{odYOE;2(�������������}tjaWND;1(�������������}sj`WMD:1' � � � � � � �     Y		X	�W		V	�U		T	�S		R	�Q		P	�O		N	�M		L	�K		J	�I		H	�G		F	�E		D	�C		B	�A		@	�?		>	�=		<	�;		:	�9		8	�7		6	�5		4	�3		2	�1		0	�/		.	�-		,	�+		*	�)		(	�'	�^�viewrynekrynekCREATE VIEW rynek AS
	SELECT
		kupon.kodwyd, kupon.kodwyn,
		nazwawyd, nazwawyn,
		sum(stawka) AS stawka
	FROM wynik LEFT OUTER JOIN kupon ON (kupon.kodwyd = wynik.kodwyd AND kupon.kodwyn = wynik.kodwyn) NATURAL JOIN wydarzenie 
	GROUP BY kupon.kodwyd, kupon.kodwyn, nazwawyd, nazwawyn
	ORDER BY kupon.kodwyd, kupon.kodwyn   N � ���������������ymdXL@4(	�����������znbVMC7+
�����������znbXOC7+ � � � � � � � � � 
N	���
M	���
L	<
K	)�
J	 /
I	���
H	���
G	���
F	<
E	;
D	 .
C	���
B	���
A	���@		?	
>	
MK
=	���
<	;
;	<
:	 .
9	���
8	���
7	���
6	;
5	<
4	 .
3	���
2	���
1	���0	
/	?
.	
ML
-	���
,	���+	�*	
)	
MM
(	��
'	�i�
&	?
%	�=	$	JX
#	���
"	���
!	���
 	?
	��
	
ML
	�=
	���
	���	
	?	
	
MH
	���
	��
	���
	;	
	 .		JX		
						   D � ����������}pcVI</"����������ym`SF9,����������zm`SF9, � � � � � � � �              
�	���
�	��
�	���	�	SR
�	���
�	
MM
�	��
�	���
�	?
�	���
�	
MK
�	���
�	���
�	;
�	<
�	 .
�	���
�	���
�	���
�	?�	�
�
	
MM
�		���
�	���
�	?
�	?
�	���
�	
MM
�	����	�
�	��
� 	?
�	?
�~	
L�
�}	��
�|	���	�{	�%
�z	�(�
�y	?
�x	?
�w	�<
�v	���
�u	���
�t	���
�s	?
�r	?
�q	
MM
�p	���
�o	���
�n	���
�m	?
�l	=
�k	���
�j	
MM
�i	���
�h	���
�g	?�f	
�e	
ML
�d	���
�c	����b	
�a	?�`	
�_	
MM
�^	����]	
�\	���   H � ����������si]QE9-!	�����������~rf]QH<0$����������~qdWL?2% � � � � � � � �             
�			
�	����	�
�	
MM
�	���
�	��
�	?
�	?
�	�>&
�	
)�
�	���
�	��
�
	����		
�	
MK
�	���
�	���
�	;
�	<
�	 .
�	���
�	���
� 	���	~		
}	
MK
|	;
{	���
z	<y	
x	 .
w	���
v	���
u	���t	
s	>r		
q	
MK
p	���
o	<
n	���
m	9
l	 1
k	���
j	���
i	���h	
g	?f	�
e	
MM
d	���
c	���
b	?
a	?
`	�Y5
_	
5
^	���
]	\p
\	���[	�
Z	
MM
Y	�=
X	���
W	>V		
U	
MK
T	���
S	���
R	;
Q	<
P	 .
O	��}   E � ���������reXM@3&����������wj]PC6)����������uh]PC6) � � � � � � � � �
�[	?�Z	�
�Y	
MM
�X	���
�W	?
�V	���	�U	ft
�T	���
�S	
ML
�R	\p
�Q	����P	�
�O	�=
�N	
MM
�M	?
�L	����K	�
�J	?
�I	�ԕ
�H	
MM
�G	���
�F	�,
�E	��~
�D	?
�C	
?
�B	�'
�A	
D�
�@	�L	�?	�
�>	���	�=	�y
�<	<
�;	;
�:	 .
�9	���
�8	���
�7	���
�6	?
�5	>
�4	
MK
�3	���
�2	���
�1	���
�0	<
�/	;
�.	 .
�-	���
�,	���
�+	����*		�)	��(	
�'	
ML
�&		p
�%	����$	
�#	
MK
�"	���
�!	���
� 	;
�	<
�	 .
�	���
�	���
�	��x	�	4
�	?
�	
K�
�	���    ����������wj]PD9,                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
�	���
�~	���
�}	��s�|	�	�{	�
�z	?
�y	
C�
�x	���	�w	�y
�v	���
�u	<
�t	;
�s	 .
�r	���
�q	���
�p	����o	��n	
�m	
MM   D � ����������vi\OC6)����������sfYL?2%����������reXK>1$ � � � � � � � � �        
�l	?
�k	����j	�
�i	?
�h	
MM
�g	���
�f	���
�e	���
�d	?	�c	SR
�b	
MM
�a	���
�`	��
�_	���
�^	?
�]	���
�\	
MK
�[	����Z	�
�Y	�ź
�X	?	�W	SQ
�V	
:F
�U	���
�T	��
�S	���
�R	?
�Q	���
�P	
MK
�O	���
�N	���
�M	<
�L	;
�K	 .
�J	���
�I	���
�H	���
�G	?
�F	?
�E	
MM
�D	���
�C	���
�B	�8
�A	�P
�@	 ��
�?	��
�>		��
�=	l�
�<	��S
�;	e�
�:	
H�
�9	���
�8	���	�7	�
�6	�i�
�5	?
�4	?
�3	�?	�2	��	�1	JY�0	�
�/	���
�.	
MM
�-	���
�,	���
�+	��H	�*	SQ
�)	?   E � ����������wj]PC6)����������{naTG:/"����������|obUH;.! � � � � � � � � �
�d	���
�c	���
�b	���
�a	<
�`	;
�_	 .
�^	���
�]	���
�\	?
�[	���
�Z	?
�Y	
MM
�X	���
�W	���
�V	���
�U	=
�T	?
�S	���
�R	
MM
�Q	���
�P	����O	�	�N	Kd
�M	
MM
�L	��
�K	=
�J	���
�I	
MM
�H	���
�G	���
�F	���
�E	?�D	
�C	
MK
�B	���
�A	;
�@	���
�?	<�>	
�=	 .	�<	��
�;	����:		�9	JX
�8	 .
�7	���
�6	���
�5	���
�4	?
�3	?
�2	
MM
�1	���
�0	���
�/	���
�.	?
�-	?
�,	���
�+	
ML
�*	���
�)	����(	
�'	���&	�
�%	
MM
�$	�=
�#	��
�"	?	�!	Kc
� 	
)�   T � ������������~tj`VLB8.$�������������zpf\RH>4* �������������vlbXND:0& � � � � � � � �	�|	�{	�z	�y	�x	�w	�v	�u	�t	�s	�r	�q	�p	�o	�n	�m	�l	�k	�j	�i	�h	�g	�f	�e	�d	�c	�b	�a	�`	�_	�^	�]	�\	�[	�Z	�Y	�X	�W	�V	�U	�T	�S	�R	�Q	�P	�O	�N	�M	�L	�K	�J	�I	�H	�G	�F	�E	�D	�C	�B	�A	�@	�?	�>	�=	�<	�;	�:	�9	�8	�7	�6	�5	�4	�3	�2	�1	�0	�/	�.	�-	�,	�+	�*	�)	   D � ���������~sfYOB5(���������rgZPC6)����������zm`SF9, � � � � � � � �             
�(	
%�
�'	��
�&	���
�%	���
�$	?
�#	���
�"	
MM
�!	���
� 	?
�	���
�	?
�	���
�	
MK
�	���
�	���
�	;
�	<
�	 .�	
�	�'
�	���	�	�
�	���
�	;
�	<
�	 .�	
�	�>&
�	���
�	��
�
	���
�		
ML
�	���
�	����	
�	?�	�
�	
MM
�	���
�	�
� 	���
�	?
�~	
MK
�}	���
�|	���
�{	���
�z	�A
�y	<
�x	 1
�w	x�
�v	���
�u	���
�t	?
�s	����r	
�q	
MM
�p	����o	�
�n	���
�m	?
�l	?
�k	
MM
�j	���
�i	���
�h	���
�g	>
�f	?
�e	
MK