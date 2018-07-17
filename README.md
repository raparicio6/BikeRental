# BikeRental

Context:
A company rents bikes under following options:
1. Rental by hour, charging $5 per hour
2. Rental by day, charging $20 a day
3. Rental by week, changing $60 a week
4. Family Rental, is a promotion that can include from 3 to 5 Rentals (of any type) with a discount
of 30% of the total price

Personally, I consider that when starting with the development of a project, the design and architecture part of it is essential. When there is a bad design, the drawbacks, such as low maintainability, classes with various responsibilities, and others, do not take long to arrive. For this reason, at the beginning of the project I took a lot of time for the design of it, which can be seen in the class diagram within the documentation.

I emphasized the SOLID principles. First of all I focused on the fact that the classes should have a unique responsibility. I decided to include interfaces that represent the different roles within the system, such as ICashier and IRentalOperator, which are in charge of validating, creating and modifying the own business classes, so that they do not increase their responsibility doing it themselves. There is also the IClient interface, which is the one that performs the different actions in the system. The roles of the employees are essential since, in my opinion, it would be a mistake for the client to create their rentals, it is not its responsibility.

Then I emphasized on the investment of dependencies. I sought to describe the main functionalities of the system based on behaviors and not implementations. I developed several interfaces to comply with the interface segregation.

At the time of developing the different costs according to the unit of time that is desired, I relied on the polymorphism, using the Strategy pattern, choosing a particular modality based on the selection made. I wanted to comply with the open/closed principle: if in the future more units of time are added, it would be enough to create the corresponding new modalities, I would not have to edit the current code. I used an inheritance relationship since I consider that in this case the parent class RentalModality defines the daughter classes, it is a very strong relationship between them.

Another thing that I consider important is the domain-driven design (DDD). As I mentioned before, most of the time I dedicated it to the armed of the model. This was the key to be clear about the logic of the domain. The application of DDD is strongly reflected in the use of a structured language that reflects the real domain of the system, and always respects it. That is, if it says "rentals", I do not use also "rents", "bike" with "bicycle", "client" with "customer", etc. For the Money class I decided to represent it as Value Object, since I have done it in the past and it seemed very advantageous to be able to model money this way within a system.

Speaking about more specific classes such as RentalModality, FamilyRentalInformation and PromotionRules, the idea of assign them as properties to Rental and FamilyRental when they are created, and not obtained with the IRentalOperator when requested, is to be able to save the value of those classes at the moment in which the Rental/FamilyRental is created, and not that the properties take the values of the current instances IRentalOperator has. That is, if the rental costs $100 at the moment that the client requests it, he should not be charged with the price the rental has at the time the client finalize it. For this reason, none of these classes have setters. If any property is updated, a new instance must be created. If a change were made, it would happen as mentioned previously and it would not be respected the established things with the Rentals y FamilyRentals already initiated.

I decided to have RentalModality, FamilyRentalInformation and PromotionRules as classes and not that their properties are directly within Rental/FamilyRental due to the fact that as the same values are active for a while, several instances of Rental/FamilyRental will "point" to the same instances of these classes, so it would be redundant to have the same data repeated for several different instances of Rental/FamilyRental.

About the RentalBeginning, RentalFinalization and Payment classes, they are necessary to save information of the different roles that intervene in the system. These last two have a property of IClient since I implemented as a business rule that not necessarily the client who requested a rental has to finalize/pay it. I thought that in the reality many times when people ride bikes in group, after a lot of riding they get tired, and a person returns to return more than one bike so another person does not have to go too. 

Also in reference to the payment, I decided to consider that another person who requested the rental can pay for it as it may happen that a child requests a rental but one of their parents pays for it. About the employees of the system, I thought it would be appropriate to have a follow-up of them.

Thinking about future expansions of the system, it can be included for example Big Data applications in it, where the previously mentioned information would be very valuable. Several conclusions can be drawn, such as how many clients do not finalize their own rental to make a decision for example to put inserts on bikes so the client can take two bikes together and transportation is facilitated, etc.

Talking about the ISale interface, I thought it was good to have a behavior for any service or product that the company has. If in the future the company wants to sell sports drinks for example, then this new object must implement the interface.

For the concept of the Role interface, I applied a strategy similar to the one used in the projects where I work in my company. I also got a more accurate idea by reading a paper by Martin Fowler: https://www.martinfowler.com/apsupp/roles.pdf.

Future improvements:
- Update the class diagram with the latest code changes.
- Have a method in ICashier that can change a currency of one type towards the same value represented in another currency, so they can be compared.
- Write the different business rules as user stories, such as:
As a client who not necessarily request a rental I want to be able to finalize it.
As a client who did not necessarily make the purchase I want to be able to pay for it.
