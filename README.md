Release Notes 1.1:

1. Assumptions:
	1.1. ** Kindly take note: This project uses C# 7.1, developed using VS2017, Framework version 4.7.1 **. Please make sure you are running code with C#7.1, Framework 4.7.1.
	
	1.2. I was not able to see if payment start date drives any logic so no logic is there that depends on the date
	
	1.3. Date is given as "Date Range" but I kept it as plain DateTime
	
	1.4. Assumptions for code logic:
	
		1.4.1. Used Strategy pattern that is based on Financial Year, currently I kept it as constant 2017, but it can be made to work as per the payment date
	
		1.4.2. So based on the tax strategy (if any), Payslip format might change, that is handled via Payslip Factory.
		
	1.5. Code uses EF code first approach, make sure you have SQLServer (Express or above). Database would be created by EF.
		
	1.6. Use of CSS, JQuery and JQuery based controls etc. is bare minimum.

	
	2. Link to source code : https://github.com/bhanuchhabra/SampleApp2

3. How to run instructions:
	
	3.1. To Run application: 
	
		A) Make sure WebApp and WebAPI both are set as startup app. To do so: 
		
		3.1.1. Right click Solution, Select Properties
		
		3.1.2. Select Multiple startup project radio button
		
		3.1.3. For WebApp and WebAPI select the Start option from drop down next to them.
		
		B)Download Nuget Packages:
		
		3.1.4. Right click solution, Click Restore Nuget packages
		
		3.1.5. Start the project
		
	3.2. At Home page, click on Create Employee or Let's Start
	
	3.3. After Creating Employee click on EmployeeList
	
	3.4. Click on Generate Payslip to see the payslip information
	
4. Test Inputs and outputs are as is stated in Unit Test Project
