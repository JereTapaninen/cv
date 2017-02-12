<?php
	require_once "encoding.php";
	use \ForceUTF8\Encoding;

	class Esitys
	{
		private $id;
		private $teema;
		private $esityspaikka;
		private $kaupunki;
		private $pvm;
		private $paikat;
		private $vapaatpaikat;

		public function Esitys($id, $teema, $esityspaikka, $kaupunki, $pvm, $paikat, $vapaatpaikat)
		{
			$this->id = $id;
			$this->teema = $teema;
			$this->esityspaikka = $esityspaikka;
			$this->kaupunki = $kaupunki;
			$this->pvm = $pvm;
			$this->paikat = $paikat;
			$this->vapaatpaikat = $vapaatpaikat;
		}

		public function getID()
		{
			return $this->id;
		}

		public function getTeema()
		{
			return $this->teema;
		}

		public function getEsityspaikka()
		{
			return $this->esityspaikka;
		}

		public function getKaupunki()
		{
			return $this->kaupunki;
		}

		public function getPvm()
		{
			return $this->pvm;
		}

		public function getPaikat()
		{
			return $this->paikat;
		}

		public function getVapaatpaikat()
		{
			return $this->vapaatpaikat;
		}
	}

	$esitykset = array();

	$servername = "127.0.0.1";
	$username = "root";
	$password = "kekmem";
	$dbname = "Sirkus";

	$conn = new mysqli($servername, $username, $password, $dbname);

	if ($conn->connect_error)
		die("Connection failed: " . $conn->connect_error);

	$sql = "SELECT esitysID, teema, esityspaikka, kaupunki, pvm, paikat, vapaatpaikat FROM esitys";
	$result = $conn->query($sql);

	if ($result->num_rows > 0)
	{
		while ($row = $result->fetch_assoc())
		{
			array_push($esitykset, new Esitys(Encoding::fixUTF8($row["esitysID"]), Encoding::fixUTF8($row["teema"]), 
											  Encoding::fixUTF8($row["esityspaikka"]), Encoding::fixUTF8($row["kaupunki"]),
											  $row["pvm"], Encoding::fixUTF8($row["paikat"]), Encoding::fixUTF8($row["vapaatpaikat"])));
		}
	}
	else
	{
		echo "ERROR: NO RESULTS";
	}

	$conn->close();

	function getEsitysWithID($arr, $id) {
		foreach ($arr as &$esitys)
		{	
			if ($esitys->getID() == $id)
				return $esitys;
		}

		return null;
	}

	function pushTilaus($sposti, $puhnro, $liput, $vapaatpaikat, $esitysID)
	{
		$servername = "127.0.0.1";
		$username = "root";
		$password = "kekmem";
		$dbname = "Sirkus";

		$conn = new mysqli($servername, $username, $password, $dbname);

		if ($conn->connect_error)
			die("Connection failed: " . $conn->connect_error);

		$sql = "INSERT INTO tilaaja (sposti, puhelin, paikkojenlkm, esitysID) VALUES ('$sposti', '$puhnro', '$liput', '$esitysID')";

		$conn->query($sql);

		$uudetVapaatpaikat = $vapaatpaikat - $liput;

		$sql = "UPDATE esitys SET vapaatpaikat='$uudetVapaatpaikat' WHERE esitysID=$esitysID";

		$conn->query($sql);

		$conn->close();
	}
?>