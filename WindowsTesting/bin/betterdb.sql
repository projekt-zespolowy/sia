SQLite format 3   @    ��            �                                                         �    	��z�                                                                                                                                                                                                                                                                  ��tablekuponkuponCREATE TABLE kupon
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
	imie VARCHAR NOT NU   � � ���                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         ˨SYSTEM��Wojtek   �mKoz	��Kozak   � �����                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Popularny��!Egzotycznyu0Konklawe@	Finał@	NULLEVENT   - ��������qcME=5-                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       	BA	BA)Inny kardynał
Ortega	BettoriAmatoBertoneTurkson
	ScolaBergoglio	)Bayern Munchen		(REMIS)	/Borussia Dortmund
   � ���������������                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              		
							   �    �������������{odXL@4(�����������ymbVJ?3'�����������{ocWL@4( � � � � � � � �
N	
MM
M	���
L	��
K	��H
J	?
I	�F
H	
%�
G	���
F	��
E	��s
D	?
C	
C�
B	�	L
A	���	@	�y
?	���
>	<
=	;
<	 .
;	���
:	���
9	���8	�7	
6	?
5	
MM
4	���3	�
2	���
1	?	0	Kd
/	
ML
.	���
-	��
,	���	+	�
*	���
)	
MK	(	�y
'	���
&	<
%	:
$	 /
#	���
"	���
!	���
 	;
	<	
	 .
	���
	���
	���
	?	
	
MH
	���
	��
	���
	;		JX
	 .			
					   
N    3  3 �S�� /                            �
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
	ORDER BY kupon.koduz, kupon.kodwyd, kupon.kodwyn                                     
   
      
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  a�viewrynekrynekCREATE VIEW rynek AS
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
	ORDER BY kupon.kodwyd, kupon.kodwyn   N � ���������������{odXL@4(�����������ymbVJ?3'�����������{ocWL@4( � � � � � � � �
N	
MM
M	���
L	��
K	��H
J	?
I	�F
H	
%�
G	���
F	��
E	��s
D	?
C	
C�
B	�	L
A	���	@	�y
?	���
>	<
=	;
<	 .
;	���
:	���
9	���8	�7	
6	?
5	
MM
4	���3	�
2	���
1	?	0	Kd
/	
ML
.	���
-	��
,	���	+	�
*	���
)	
MK	(	�y
'	���
&	<
%	:
$	 /
#	���
"	���
!	���
 	;
	<	
	 .
	���
	���
	���
	?	
	
MH
	���
	��
	���
	;		JX
	 .			
						   � ���������sfYL?4(����������{naTG:-                                                                                                                                                                                                                                                                                                                                                                                                                                               
�	���
� 	���
�	 ˲
�~	?
�}	
MM
�|	���
�{	�4P
�z	���
�y	?�x	
�w	
ML
�v	
�0	?
�/	
MM
�.	���
�-	���
�,	���
�+	?
�*	=
�)	
MM
�(	���
�'	���
�&	���	�%	Kd�$	�
�#	
MM
�"	��
�!	?
� 	���	�	��
�	
MM
�	���
�	�=
�	���
�	?	�	[
�	
MK
�	���
�	 ȯ
�	���   6} �����������ti]QE9-!	�����������{ocWK?5)����������}                                                                                                                                                                                                                                                                         
�	���
�	���	�	�y
�	<
� 	;
	 .
~	���
}	���
|	���
{	?
z	?
y	���
x	
ML
w	���
v	���u	�
t	?
s	?
r	
MM
q	���
p	���
o	���
n	?	m	ft
l	
MK
k	���
j	\s
i	���
h	;	g	JX
f	 .
e	���
d	���
c	���
b	?
a	 �[
`	
MM
_	���
^	��
]	��H
\	?	[	SR
Z	
%�
Y	���
X	��
W	���
V	<U	
T	 .
S	���
R	���
Q	���
P	?	O	SR   E � ���������reXM@3&����������wj]PC6)����������uh]PC6) � � � � � � � � �
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
�	���   G � ����������~rfZQE9-!	�����������{ocWK?3'����������{naTG:-  � � � � � � � �        
�	;
�	?3
�	 .
�	���
�	���
�	���
�	?
�	?
�	
MK
�	���
�
	���
�		���
�	;
�	<
�	 .
�	���
�	���
�	���
�	?
�	?
� 	
MM
	���
~	���
}	���
|	?
{	?
z	
K
y	��
x	���
w	���
v	 ȯ
u	;
t	?3
s	 .
r	���
q	���
p	���
o	:
n	<
m	 /
l	���
k	���
j	�i�
i	��
h	��
g	�=	f	eU	e	JX
d	���
c	���
b	
MK
a	���
`	;
_	���
^	<
]	 .\	
[	���
Z	���
Y	���
X	?
W	�
V	
MC
U	��4
T	���
S	��R	�
Q	?
P	P�
O		i�
N	���   D � ����������tg]PC9,����������zm`SF9,����������vi\OB5( � � � � � � � �          
�	���
�	?
�	�Y5
�	
5
�	���
�	\s
�	���
�	;	�	JX
�	 .
�	���
�	���
�	���
�	?
�	?
�	
MM
�	���
�	���
�
	���
�		?
�	?
�	
MK
�	���
�	���
�	���
�	;
�	<
�	 .
� 	���
�	���
�~	���
�}	?
�|	?
�{	
MM
�z	���
�y	���
�x	���
�w	?
�v	 ��
�u	
MM
�t	���
�s	�4A
�r	���
�q	?	�p	�
�o	
L�
�n	���
�m		p
�l	����k	
�j	
MM
�i	����h	
�g	���
�f	?�e	
�d	
ML
�c	����b	�
�a	��x	�`	4
�_	?
�^	
K�
�]			
�\	��>
�[	���
�Z	�
�Y	�Y3   E � ����������wj]PC6)����������{naTG:/"����������|obUH;.! � � � � � � � � �
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
)�   H � ����������}qeYMA5, �����������xl`TH<0$����������{nbUJ=0#  � � � � � � � �   
�	 1
�	���
�	���
�	���
�	?�	
�	
ML
�	����	�
�	����	
�
	?
�		
ML
�	����	�
�	��H	�	SQ
�	?
�	
%�
�	��
�	���
� 	��H
	?
~	�F
}	
%�
|	���
{	��
z	���
y	<
x	 .w	
v	���
u	���
t	���
s	?
r	?
q	
MK
p	���
o	���
n	���
m	;
l	<
k	 .
j	���
i	���
h	��h
g	?
f	?
e	
H�
d	���
c	���
b	*
a	���
`	<_	
^	 .
]	���
\	���
[	��~
Z	?
Y	?
X	
D�
W	�'
V	���
U	���	T	�
S	;
R	<
Q	 .
P	�	L
O	���
N	���   D � ���������~reXK>1$
����������}pcVI</"����������yl_SF9, � � � � � � � � �   
�a	����`	�
�_	?
�^	?
�]	
ML
�\	���
�[	���
�Z	���	�Y	fs
�X	?
�W	
5
�V	���
�U	\s
�T	���
�S	;	�R	JX
�Q	 .
�P	���
�O	���
�N	�(�
�M	?
�L	?
�K	�;
�J	���
�I	���
�H	���
�G	;
�F	<
�E	 .
�D	���
�C	���
�B	���
�A	9
�@	<
�?	 1
�>	���
�=	���
�<	����;	�
�:	?
�9	?
�8	
MM
�7	���
�6	���
�5	���
�4	?
�3	>
�2	
MK
�1	���
�0	���
�/	���
�.	<
�-	;
�,	 .
�+	�'
�*	���
�)	���	�(	�
�'	;
�&	<
�%	 .
�$	���
�#	���
�"	��H
�!	?
� 	?
�	�F
�	
%�