// JScript source code
	var DayName=["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"];
	
	
	
	var oneMinute=1000*60;
	
	var intervalObject=new Object();
	intervalObject["yyyy"]={units:1000*60*60*24*365,measure:"year"};
	intervalObject["m"]={units:1000*60*60*24*30,measure:"month"};
	intervalObject["d"]={units:1000*60*60*24,measure:"day"};
	intervalObject["Q"]={units:intervalObject["m"].units*3,measure:"quarter"};
	intervalObject["H"]={units:oneMinute*60,measure:"hour"};
	intervalObject["N"]={units:oneMinute,measure:"minute"};
	intervalObject["S"]={units:1000,measure:"second"};
	
	
	function DateDiff(dateAddObj){
		this.interval=dateAddObj.interval;
		this.date1=dateAddObj.date1;
		this.date2=dateAddObj.date2;
		this.calculate=calculate;
		this.calculate();
	}

	Date.prototype.DateDiff=DateDiff;
	
	
	
	
	function calculate(){
		var paramDate1=new String(this.date1);
		splitDate1=paramDate1.split("-");
		paramDateYear1=splitDate1[0];
		paramDateMonth1=splitDate1[1]-1;
		paramDateDay1=splitDate1[2];
		if(paramDateMonth1>12){
			alert("Invalid Month!");
			return false;
		}
		if(paramDateDay1>31){
			alert("Invalid Day!");
			return false;
		}
		
		
		var paramDate2=new String(this.date2);
		splitDate2=paramDate2.split("-");
		paramDateYear2=splitDate2[0];
		paramDateMonth2=splitDate2[1]-1;
		paramDateDay2=splitDate2[2];
		if(paramDateMonth2>12){
			alert("Invalid Month!");
			return false;
		}
		if(paramDateDay2>31){
			alert("Invalid Day!");
			return false;
		}
		
				
		var paramDate1Object=new Date(paramDateYear1,paramDateMonth1,paramDateDay1);
		paramDate1Object.setHours(0);
		paramDate1Object.setMinutes(0);
		paramDate1Object.setSeconds(0);
		//paramDate1Object.getTimezoneOffset() * oneMinute;
		var paramDate1ObjectTime=paramDate1Object.getTime();
		
		
		var paramDate2Object=new Date(paramDateYear2,paramDateMonth2,paramDateDay2);
		paramDate2Object.setHours(0);
		paramDate2Object.setMinutes(0);
		paramDate2Object.setSeconds(0);
		var paramDate2ObjectTime=paramDate2Object.getTime();
		
		if(paramDate2Object>paramDate1Object){
			DSTAdjust=(paramDate2Object.getTimezoneOffset() - paramDate1Object.getTimezoneOffset()) * oneMinute;
			
		}
		else{
			DSTAdjust=(paramDate1Object.getTimezoneOffset() - paramDate2Object.getTimezoneOffset()) * oneMinute;
		}
		
		if(typeof intervalObject[this.interval]!="undefined"){
			if(typeof intervalObject[this.interval].units=="undefined"){
				alert("Interval is invalid!");
				return false;
			}
			
			var diff=Math.abs(paramDate2ObjectTime-paramDate1ObjectTime) - DSTAdjust;
			var timeDiff=Math.floor(diff/intervalObject[this.interval].units);
			if(timeDiff>1){
				var rname=intervalObject[this.interval].measure + "s";
			}
			else{
				var rname=intervalObject[this.interval].measure;
			}
			
			this.difference=parseInt(timeDiff);
		}
		else{
			this.difference="Wrong format of interval!";
		
		}
		
	}

