

select firstName, lastName
from Client
where lastName like 'La%';

select accountNumber
from Account
where balance=0.00;

select accountNumber
from SecurityHolding
where type='stock' and code='22';

select accountNumber
from Account
Except select accountNumber from SecurityHolding;

select accountType, count(*) as count
from Account
group by AccountType;

select firstName, lastName, email, HKIDPassportNumber
from Client, SecurityHolding
where Client.accountNumber=SecurityHolding.accountNumber and
SecurityHolding.type='stock' and SecurityHolding.code='22' 
order by lastName;

