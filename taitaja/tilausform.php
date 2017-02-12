<form method="POST">
	<h2>Tilauksen vahvistus</h2>

	<input type="text" name="esitys" id="esitysID" value="<?php echo $esitysID; ?>" style="display: none; visibility: hidden" readonly />

	<div>
		<div>
			<label for="teema">Esityksen teema</label>
			<p id="teema" name="kekmem"><?php echo $esitys->getTeema(); ?></p>
			<input type="text" name="teema" id="teemaText" value="<?php echo $esitys->getTeema(); ?>" style="display: none; visibility: hidden" readonly />
		</div>
		<div>
			<label for="esityspaikka">Esityksen paikka</label>
			<p id="esityspaikka"><?php echo $esitys->getEsityspaikka(); ?></p>
			<input type="text" name="esityspaikka" id="esityspaikkaText" value="<?php echo $esitys->getEsityspaikka(); ?>" style="display: none; visibility: hidden" readonly />
		</div>
		<div>
			<label for="kaupunki">Kaupunki</label>
			<p id="kaupunki"><?php echo $esitys->getKaupunki(); ?></p>
			<input type="text" name="kaupunki" id="kaupunkiText" value="<?php echo $esitys->getKaupunki(); ?>" style="display: none; visibility: hidden" readonly />
		</div>
		<div>
			<label for="paivamaara">Päivämäärä</label>
			<p id="paivamaara"><?php echo $esitys->getPvm(); ?></p>
			<input type="text" name="paivamaara" id="paivamaaraText" value="<?php echo $esitys->getPvm(); ?>" style="display: none; visibility: hidden" readonly />
		</div>
		<div>
			<label for="paikat">Paikat</label>
			<p id="paikat"><?php echo $esitys->getPaikat(); ?></p>
			<input type="text" name="paikat" id="paikatText" value="<?php echo $esitys->getPaikat(); ?>" style="display: none; visibility: hidden" readonly />
		</div>
		<div>
			<label for="vapaatpaikat">Vapaat paikat</label>
			<p id="vapaatpaikat"><?php echo $esitys->getVapaatpaikat(); ?></p>
			<input type="text" name="vapaatpaikat" id="vapaatpaikatText" value="<?php echo $esitys->getVapaatpaikat(); ?>" style="display: none; visibility: hidden" readonly />
		</div>
	</div>
	<div>
		<div>
			<label for="puhnro">Puhelinnumerosi</label>
			<input type="text" id="puhnro" name="puhnro" maxlength="14" style="width: 18vmin" value="" required />
		</div>
		<div>
			<label for="email">Sähköpostiosoitteesi</label>
			<input type="email" id="email" name="email" maxlength="32" style="width: 18vmin" value="" required />
		</div>
		<div>
			<label for="liput">Lippujen lukumäärä</label>
			<input type="number" id="liput" name="liput" min="1" value="1" max="<?php echo $esitys->getVapaatpaikat(); ?>" style="width: 18vmin" value="0" required />
		</div>
	</div>
	<div id="last">
		<button onclick="function() { window.location.href = './index.php'; }" formnovalidate>Peruuta</button>
		<input type="submit" name="submit" value="Tilaa liput!" />
	</div>
</form>